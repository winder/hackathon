package main

import (
	"fmt"

	"github.com/algorand/go-algorand-sdk/client/algod"
	"github.com/algorand/go-algorand-sdk/client/kmd"
	"github.com/algorand/go-algorand-sdk/transaction"
)

// CHANGE ME

// Algorand Hackathon
const algodAddress = "http://hackathon.algodev.network:9100"
const algodToken = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"

// your own node
// const algodAddress = "http://127.0.0.1:8080"
// const algodToken = "your algodToken"

const kmdAddress = "http://127.0.0.1:7833"
const kmdToken = "your kmdToken"

// Purestake
// const algodAddress = "https://testnet-algorand.api.purestake.io/ps1"
// const algodToken = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab"

func main() {

	// Create a kmd client
	kmdClient, err := kmd.MakeClient(kmdAddress, kmdToken)
	if err != nil {
		fmt.Printf("failed to make kmd client: %s\n", err)
		return
	}
	fmt.Println("Made a kmd client")

	// // uncomment if using Purestake
	// var headers []*algod.Header
	// headers = append(headers, &algod.Header{"X-API-Key", algodToken})
	// // Create an algod client
	// algodClient, err := algod.MakeClientWithHeaders(algodAddress, "", headers)
	// if err != nil {
	// 	fmt.Printf("failed to make algod client: %s\n", err)
	// 	return
	// }

	// Create an algod client
	// Comment if using Purestake
	algodClient, err := algod.MakeClient(algodAddress, algodToken)
	if err != nil {
		fmt.Printf("failed to make algod client: %s\n", err)
		return
	}

	fmt.Println("Made an algod client")

	// Get the list of wallets
	listResponse, err := kmdClient.ListWallets()
	if err != nil {
		fmt.Printf("error listing wallets: %s\n", err)
		return
	}

	// Find our wallet name in the list
	var exampleWalletID string
	fmt.Printf("Got %d wallet(s):\n", len(listResponse.Wallets))
	for _, wallet := range listResponse.Wallets {
		fmt.Printf("ID: %s\tName: %s\n", wallet.ID, wallet.Name)
		if wallet.Name == "testwallet" {
			fmt.Printf("found wallet '%s' with ID: %s\n", wallet.Name, wallet.ID)
			exampleWalletID = wallet.ID
		}
	}
	// Get a wallet handle
	initResponse, err := kmdClient.InitWalletHandle(exampleWalletID, "testpassword")
	if err != nil {
		fmt.Printf("Error initializing wallet handle: %s\n", err)
		return
	}

	// Extract the wallet handle
	exampleWalletHandleToken := initResponse.WalletHandleToken

	// Generate a new address from the wallet handle
	gen1Response, err := kmdClient.ListKeys(exampleWalletHandleToken)
	if err != nil {
		fmt.Printf("Error generating key: %s\n", err)
		return
	}
	fmt.Printf("Generated address 1 %s\n", gen1Response.Addresses[0])
	fromAddr := gen1Response.Addresses[0]

	gen2Response, err := kmdClient.GenerateKey(exampleWalletHandleToken)
	if err != nil {
		fmt.Printf("Error generating key: %s\n", err)
		return
	}
	fmt.Printf("Generated address 2 %s\n", gen2Response.Address)
	// gen2Response.Address = "RXGGL7M2XRGODLM6ACDOWBZRJLZ4FTY3NSTTMHL6M2YUG7JGANYY6X2HSA"
	toAddr := gen2Response.Address

	// Get the suggested transaction parameters
	txParams, err := algodClient.SuggestedParams()
	if err != nil {
		fmt.Printf("error getting suggested tx params: %s\n", err)
		return
	}

	// Make transaction
	genID := txParams.GenesisID
	tx, err := transaction.MakePaymentTxn(fromAddr, toAddr, 1, 100000, txParams.LastRound, txParams.LastRound+100, nil, "", genID, txParams.GenesisHash)
	if err != nil {
		fmt.Printf("Error creating transaction: %s\n", err)
		return
	}

	// Sign the transaction - change to your wallet password
	signResponse, err := kmdClient.SignTransaction(exampleWalletHandleToken, "yourpassword", tx)

	if err != nil {
		fmt.Printf("Failed to sign transaction with kmd: %s\n", err)
		return
	}

	fmt.Printf("kmd made signed transaction with bytes: %x\n", signResponse.SignedTransaction)

	// Broadcast the transaction to the network
	// **** Note that this transaction will get rejected because the accounts do not have any tokens
	// **** copy off the Generated address 1 in the output below and past into the testnet dispenser
	// https://bank.testnet.algorand.network/
	txHeaders := append([]*algod.Header{}, &algod.Header{"Content-Type", "application/x-binary"})
	sendResponse, err := algodClient.SendRawTransaction(signResponse.SignedTransaction, txHeaders...)
	if err != nil {
		fmt.Printf("failed to send transaction: %s\n", err)
		return
	}

	fmt.Printf("Transaction ID: %s\n", sendResponse.TxID)
}
