package main

import (
	"context"
	"encoding/json"
	"fmt"

	"github.com/algorand/go-algorand-sdk/client/v2/indexer"
)

// indexer host
const indexerAddress = "http://localhost:8981"
const indexerToken = ""

// query parameters
var minAmount uint64 = 10

// var data = "showing prefix"
// var encodedNote = base64.StdEncoding.EncodeToString([]byte(data))
var notePrefix = "Russ"
var round uint64 = 11563027
func main() {

	// Create an indexer client
	indexerClient, err := indexer.MakeClient(indexerAddress, indexerToken)
	if err != nil {
		return
	}

	// Query
	result, err := indexerClient.SearchForTransactions().MinRound(round).NotePrefix([]byte(notePrefix)).Do(context.Background())

	// Print the results
	JSON, err := json.MarshalIndent(string(result.Transactions[0].Note), "", "\t")
	fmt.Printf(string(JSON) + "\n")
}
