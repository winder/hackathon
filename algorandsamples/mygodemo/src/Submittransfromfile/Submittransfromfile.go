package main

import (
	"encoding/gob"
	"fmt"
	"os"

	"github.com/algorand/go-algorand-sdk/client/algod"
)

// CHANGE ME

// Algorand Hackathon
const algodAddress = "http://hackathon.algodev.network:9100"
const algodToken = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"

// your own node
// const algodAddress = "http://127.0.0.1:8080"
// const algodToken = "your algodToken"

// Purestake
// const algodAddress = "https://testnet-algorand.api.purestake.io/ps1"
// const algodToken = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab"

func main() {

	var signedTransaction []byte
	file, err := os.Open("./stx.gob")
	if err == nil {
		decoder := gob.NewDecoder(file)
		err = decoder.Decode(&signedTransaction)
	} else {
		fmt.Printf("failed to open signed transaction: %s\n", err)
		return
	}
	file.Close()
	// Create an algod client
	algodClient, err := algod.MakeClient(algodAddress, algodToken)
	if err != nil {
		fmt.Printf("failed to make algod client: %s\n", err)
		return
	}
	// // uncomment if using Purestake
	// var headers []*algod.Header
	// headers = append(headers, &algod.Header{"X-API-Key", algodToken})
	// // Create an Purestake algod client
	// algodClient, err := algod.MakeClientWithHeaders(algodAddress, "", headers)
	// if err != nil {
	// 	fmt.Printf("failed to make algod client: %s\n", err)
	// 	return
	// }

	// Broadcast the transaction to the network
	txHeaders := append([]*algod.Header{}, &algod.Header{"Content-Type", "application/x-binary"})
	sendResponse, err := algodClient.SendRawTransaction(signedTransaction, txHeaders...)

	if err != nil {
		fmt.Printf("failed to send transaction: %s\n", err)
		return
	}

	fmt.Printf("Transaction ID: %s\n", sendResponse.TxID)
}
