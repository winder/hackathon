package main

import (
	"fmt"

	"github.com/algorand/go-algorand-sdk/client/algod"
	"github.com/algorand/go-algorand-sdk/crypto"
	"github.com/algorand/go-algorand-sdk/mnemonic"
	"github.com/algorand/go-algorand-sdk/transaction"
)

const algodAddress = "https://testnet-algorand.api.purestake.io/ps1"
const psToken = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab"

// Initalize throw-away account for this example - check that is has funds before running the program.
const fromAddr = "U2VHSZL3LNGATL3IBCXFCPBTYSXYZBW2J4OGMPLTA4NA2CB4PR7AW7C77E"
const mn = "code thrive mouse code badge example pride stereo sell viable adjust planet text close erupt embrace nature upon february weekend humble surprise shrug absorb faint"

const toAddr = "AEC4WDHXCDF4B5LBNXXRTB3IJTVJSWUZ4VJ4THPU2QGRJGTA3MIDFN3CQA"

func main() {
	// Create an algod client
	var headers []*algod.Header
	headers = append(headers, &algod.Header{"X-API-Key", psToken})
	algodClient, err := algod.MakeClientWithHeaders(algodAddress, "", headers)
	if err != nil {
		fmt.Printf("failed to make algod client: %s\n", err)
		return
	}
	fmt.Println("Algod client created")

	// Recover private key from the mnemonic
	fromAddrPvtKey, err := mnemonic.ToPrivateKey(mn)
	if err != nil {
		fmt.Printf("error getting suggested tx params: %s\n", err)
		return
	}
	fmt.Println("Private key recovered from mnemonic")

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

	// Sign the Transaction
	_, bytes, err := crypto.SignTransaction(fromAddrPvtKey, tx)
	if err != nil {
		fmt.Printf("Failed to sign transaction: %s\n", err)
		return
	}

	// Broadcast the transaction to the network
	txHeaders := append([]*algod.Header{}, &algod.Header{"Content-Type", "application/x-binary"})
	sendResponse, err := algodClient.SendRawTransaction(bytes, txHeaders...)
	if err != nil {
		fmt.Printf("failed to send transaction: %s\n", err)
		return
	}

	fmt.Printf("Transaction successful with ID: %s\n", sendResponse.TxID)
}