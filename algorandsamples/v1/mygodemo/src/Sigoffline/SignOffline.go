package main

import (
	"encoding/gob"
	"fmt"
	"os"

	"github.com/algorand/go-algorand-sdk/mnemonic"

	"github.com/algorand/go-algorand-sdk/crypto"
	"github.com/algorand/go-algorand-sdk/transaction"
)

func main() {

	account := crypto.GenerateAccount()
	fmt.Printf("account address: %s\n", account.Address)

	m, err := mnemonic.FromPrivateKey(account.PrivateKey)
	fmt.Printf("backup phrase = %s\n", m)
	fmt.Printf("account address: %s\n", account.PrivateKey)
	// Sign a sample transaction using this library, *not* kmd
	//This transaction will not be valid as the example parameters will most likely not be valid
	//You can use the algod client to get suggested values for the fee, first and last rounds, and genesisID
	tx, err := transaction.MakePaymentTxn(account.Address.String(), "4MYUHDWHWXAKA5KA7U5PEN646VYUANBFXVJNONBK3TIMHEMWMD4UBOJBI4", 1000, 400, 642715, 643715, nil, "", "", []byte("JgsgCaCTqIaLeVhyL6XlRu3n7Rfk2FxMeK+wRSaQ7dI="))

	if err != nil {
		fmt.Printf("Error creating transaction: %s\n", err)
		return
	}
	fmt.Printf("Made unsigned transaction: %+v\n", tx)
	fmt.Println("Signing transaction with go-algo-sdk library function (not kmd)")
	//Sign the Transaction
	txid, stx, err := crypto.SignTransaction(account.PrivateKey, tx)
	if err != nil {
		fmt.Printf("Failed to sign transaction: %s\n", err)
		return
	}
	//Save the signed object to disk
	fmt.Printf("Made signed transaction with TxID %s: %x\n", txid, stx)
	file, err := os.Create("./stx.gob")
	if err == nil {
		encoder := gob.NewEncoder(file)
		encoder.Encode(stx)
	}
	file.Close()
	if err == nil {
		fmt.Printf("Saved signed transaction to file\n")
		return
	}
	fmt.Printf("Failed in saving trx to file, error %s\n", err)

}
