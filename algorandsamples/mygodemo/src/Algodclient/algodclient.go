package main

import (
	"encoding/json"
	"fmt"

	"github.com/algorand/go-algorand-sdk/client/algod"
)

// These constants represent the algod REST endpoint and the corresponding
// API token. You can retrieve these from the `algod.net` and `algod.token`
// files in the algod data directory.

//devrel
// const algodToken = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
// const algodAddress = "http://34.216.72.17:8180"

// Algorand Hackathon
// const algodAddress = "http://hackathon.algodev.network:9100"
// const algodToken = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"

// your own node
// const algodAddress = "http://127.0.0.1:8080"
// const algodToken = "your algodToken"

// Purestake
const algodAddress = "https://testnet-algorand.api.purestake.io/ps1"
const algodToken = "B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab"

func main() {
	// Create an algod client
// uncomment for Purestake
	var headers []*algod.Header
	headers = append(headers, &algod.Header{"X-API-Key", algodToken})
	// Create an algod client
	algodClient, err := algod.MakeClientWithHeaders(algodAddress, "", headers)
	if err != nil {
		fmt.Printf("failed to make algod client: %s\n", err)
		return
	}
// comment for Purestake
	// algodClient, err := algod.MakeClient(algodAddress, algodToken)
	// if err != nil {
	// 	fmt.Printf("failed to make algod client: %s\n", err)
	// 	return
	// }

	// Print algod status
	nodeStatus, err := algodClient.Status()
	if err != nil {
		fmt.Printf("error getting algod status: %s\n", err)
		return
	}

	fmt.Printf("algod last round: %d\n", nodeStatus.LastRound)
	fmt.Printf("algod time since last round: %d\n", nodeStatus.TimeSinceLastRound)
	fmt.Printf("algod catchup: %d\n", nodeStatus.CatchupTime)
	fmt.Printf("algod latest version: %s\n", nodeStatus.LastVersion)

	// Fetch block information
	lastBlock, err := algodClient.Block(nodeStatus.LastRound)
	if err != nil {
		fmt.Printf("error getting last block: %s\n", err)
		return
	}

	// Print the block information
	fmt.Printf("\n-----------------Block Information-------------------\n")
	blockJSON, err := json.MarshalIndent(lastBlock, "", "\t")
	if err != nil {
		fmt.Printf("Can not marshall block data: %s\n", err)
	}
	fmt.Printf("%s\n", blockJSON)
}
