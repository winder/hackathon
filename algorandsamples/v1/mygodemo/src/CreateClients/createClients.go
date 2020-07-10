package main

import (
	"fmt"

	"github.com/algorand/go-algorand-sdk/client/algod"
	"github.com/algorand/go-algorand-sdk/client/kmd"
)

// Algorand Hackathon
const algodAddress = "http://hackathon.algodev.network:9100"
const algodToken = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"

// your own node
// const algodAddress = "http://127.0.0.1:8080"
// const algodToken = "your algodToken"

// Purestake
// const algodAddress = "https://testnet-algorand.api.purestake.io/ps1"
// const algodToken = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab"

const kmdAddress = "http://localhost:7833"

// const kmdToken = "your kmdToken"
const kmdToken = "456832036a18a0ff54be451b611ee435e700fb967810f170d4264bbe2af969c2"

func main() {

	var headers []*algod.Header
	headers = append(headers, &algod.Header{"X-API-Key", algodToken})
	// Create an algod purestake client
	// algodClient, err := algod.MakeClientWithHeaders(algodAddress, "", headers)
	// if err != nil {
	// 	fmt.Printf("failed to make algod client: %s\n", err)
	// 	return
	// }
	// Create an algod client
	algodClient, err := algod.MakeClient(algodAddress, algodToken)
	if err != nil {
		return
	}

	// Create a kmd client
	kmdClient, err := kmd.MakeClient(kmdAddress, kmdToken)
	if err != nil {
		return
	}

	fmt.Printf("algod: %T, kmd: %T\n", algodClient, kmdClient)

}
