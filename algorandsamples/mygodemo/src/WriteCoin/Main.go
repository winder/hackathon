package main

import (
	"algorand-l2-examplecoin/examplecoin"
	_ "encoding/gob"
	"flag"
	"fmt"
	"os"

	"github.com/algorand/go-algorand-sdk/client/algod"
	"github.com/algorand/go-algorand-sdk/crypto"
	"github.com/algorand/go-algorand-sdk/mnemonic"
	"github.com/algorand/go-algorand-sdk/transaction"
)

// this needs to be setup first https://medium.com/algorand/l2-applications-on-algorand-make-your-own-coin-6d62c5d5578d
//

var verboseFlag = flag.Bool("verbose", false, "Print extra debug info during operation.")

// Algorand Hackathon
const algodAddress = "http://hackathon.algodev.network:9100"
const algodToken = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"

// your own node
// const algodAddress = "http://127.0.0.1:8080"
// const algodToken = "your algodToken"

// account where Evan coins are created
var mastercoinKey = "PEQLYX6XAM775SZ3B6JWT5WT64VQLUIDXB54IXTROTW3S2YNRC53K7YUYQ"

// var firstRound = flag.Uint64("firstround", 828001, "the first round from which to start scanning")
// var lastRound = flag.Uint64("lastround", 829001, "the last round at which to stop scanning")

func main() {
	// must transfer 100000 first to this account from TestNet Dispenser to satify minimum requirement of reveiver balance
	//  https://bank.testnet.algorand.network/
	// to get the backup phrase use goal account export
	//	goal account export -a PEQLYX6XAM775SZ3B6JWT5WT64VQLUIDXB54IXTROTW3S2YNRC53K7YUYQ -d data -w testwall
	//  Exported key for account PEQLYX6XAM775SZ3B6JWT5WT64VQLUIDXB54IXTROTW3S2YNRC53K7YUYQ: "hockey razor script dial worry vital member basic tail mad armor bean leaf staff bargain brass evil satisfy frequent dragon deal never blue abstract east"

	receiverKey := "MZERLMKA74OWTGAORNP7BFYS4Q63NRE2PETQ3NQY7VKGXJEWWRDSW3UGTU"

	backupPhrase := "hockey razor script dial worry vital member basic tail mad armor bean leaf staff bargain brass evil satisfy frequent dragon deal never blue abstract east"

	//total token supply
	supply := uint64(100)

	// create token in note field
	// produces an Initialize struct, wrapped in the NoteField struct, and encoded in base64 bytes
	initNoteBytes64 := examplecoin.BuildInitializeNote(supply)

	//initNoteString, _ := initNoteBytes64.MarshalText()
	amountToSend := supply / 2
	// Get the suggested transaction parameters
	fromAddr := mastercoinKey
	toAddr := receiverKey

	// Create an algod client
	algodClient, err := algod.MakeClient(algodAddress, algodToken)
	if err != nil {
		fmt.Printf("failed to make algod client: %s\n", err)
		return
	}
	txParams, err := algodClient.SuggestedParams()
	if err != nil {
		fmt.Printf("error getting suggested tx params: %s\n", err)
		return
	}

	//curRound := *firstRound
	//finalRound := *lastRound

	// get privatekey from mnumonic
	sk, err := mnemonic.ToPrivateKey(backupPhrase)
	fmt.Printf("backup phrase = %s\n", sk)
	//makepayment transaction w initNoteBytes64 for the note field (this will register how many evan coins we have to start with)
	initExamplecoinTxn, err := transaction.MakePaymentTxn(fromAddr, toAddr, txParams.Fee, 0, txParams.LastRound, txParams.LastRound+1000, initNoteBytes64, "", txParams.GenesisID, txParams.GenesisHash)

	if err != nil {
		fmt.Printf("Error creating inittransaction: %s\n", err)
		return
	}
	fmt.Fprintf(os.Stderr, "initExamplecoinTxn looks like %v    \n", initExamplecoinTxn)

	//Sign the init Transaction
	inittxid, initstx, err := crypto.SignTransaction(sk, initExamplecoinTxn)
	if err != nil {
		fmt.Printf("Failed to sign inittransaction: %s\n", err)
		return
	}

	fmt.Printf("Made signed intitransaction with initTxID %s: %x\n", inittxid, initstx)
	// broadcast the init transaction
	initsendResponse, err := algodClient.SendRawTransaction(initstx)
	if err != nil {
		fmt.Printf("failed to send transaction: %s\n", err)
		return
	}
	fmt.Printf("Transaction ID: %s\n", initsendResponse.TxID)

	transferNoteBytes64 := examplecoin.BuildTransferNote(amountToSend, mastercoinKey, receiverKey)

	// todo
	// transferNoteString, _ := transferNoteBytes64.MarshalText()
	fmt.Fprintf(os.Stderr, "transferNoteBytes64 looks like %v    \n", transferNoteBytes64)
	fmt.Fprintf(os.Stderr, "initNoteBytes64 looks like %v    \n", initNoteBytes64)
	// fmt.Fprintf(os.Stderr, "transferNote looks like %v   \n", string(transferNoteString))

	// flag.Parse()
	// Make transaction.  see the godoc for general transacting information.

	//curRound := *firstRound
	//finalRound := *lastRound
	// use transferNoteBytes64 for the note field
	transferExamplecoinTxn, err := transaction.MakePaymentTxn(fromAddr, toAddr, txParams.Fee, 0, txParams.LastRound, txParams.LastRound+1000, transferNoteBytes64, "", txParams.GenesisID, txParams.GenesisHash)
	if err != nil {
		fmt.Printf("Error creating transaction: %s\n", err)
		return
	}

	fmt.Fprintf(os.Stderr, "transferExamplecoinTxn looks like %v    \n", transferExamplecoinTxn)

	fmt.Printf("Made unsigned transaction: %+v\n", transferExamplecoinTxn)
	fmt.Println("Signing transaction with go-algo-sdk library function (not kmd)")
	//Sign the Transaction
	txid, stx, err := crypto.SignTransaction(sk, transferExamplecoinTxn)
	if err != nil {
		fmt.Printf("Failed to sign transaction: %s\n", err)
		return
	}
	//Save the signed object to disk
	fmt.Printf("Made signed transaction with TxID %s: %x\n", txid, stx)
	// file, err := os.Create("../stx.gob")
	// if err == nil {
	// 	encoder := gob.NewEncoder(file)
	// 	encoder.Encode(stx)
	// }
	// file.Close()

	// if err != nil {
	// 	fmt.Printf("Failed in saving trx to file, error %s\n", err)
	// }

	// fmt.Printf("Saved signed transaction to file\n")

	// var signedTransaction []byte
	// file2, err := os.Open("../stx.gob")
	// if err == nil {
	// 	decoder := gob.NewDecoder(file2)
	// 	err = decoder.Decode(&signedTransaction)
	// } else {
	// 	fmt.Printf("failed to open signed transaction: %s\n", err)
	// 	return
	// }
	// file2.Close()

	// fmt.Printf("Opened signed transaction to file\n")
	// Broadcast the transaction to the network

	//	sendResponse, err := algodClient.SendRawTransaction(signedTransaction)
	sendResponse, err := algodClient.SendRawTransaction(stx)
	if err != nil {
		fmt.Printf("failed to send transaction: %s\n", err)
		return
	}
	fmt.Printf("Transaction ID: %s\n", sendResponse.TxID)

	// tx, err := algodClient.TransactionInformation(receiverKey, sendResponse.TxID)
	// if err != nil {
	// 	fmt.Printf("failed to get transaction information: %s\n", err)
	// 	return
	// }
	// fmt.Printf("Transaction Info Note: %s\n", tx.Note)

	// now decode the notefield for display

	var note examplecoin.NoteField
	examplecoin.ReadTransferNote(transferExamplecoinTxn.Note, &note)
	if err != nil {
		fmt.Printf("failed to decode note: %s\n", err)
		return
	}
	fmt.Printf("Note: %v\n", note)
	return
}
