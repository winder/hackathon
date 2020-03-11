package main

import (
	"fmt"
	"io/ioutil"

	"github.com/algorand/go-algorand-sdk/crypto"
	"github.com/algorand/go-algorand-sdk/transaction"
	"github.com/algorand/go-algorand-sdk/types"
)

// CHANGE ME

const kmdAddress = "http://localhost:7833"
const kmdToken = "your kmdToken"

func main() {
	addr1 := crypto.GenerateAccount()
	addr2 := crypto.GenerateAccount()
	//addr1, _ := types.DecodeAddress("DN7MBMCL5JQ3PFUQS7TMX5AH4EEKOBJVDUF4TCV6WERATKFLQF4MQUPZTA")
	//addr2, _ := types.DecodeAddress("BFRTECKTOOE7A5LHCF3TTEOH2A7BW46IYT2SX5VP6ANKEXHZYJY77SJTVM")
	addr3, _ := types.DecodeAddress("47YPQTIGQEO7T4Y4RWDYWEKV6RTR2UNBQXBABEEGM72ESWDQNCQ52OPASU")
	ma, err := crypto.MultisigAccountWithParams(1, 2, []types.Address{
		addr1.Address,
		addr2.Address,
		addr3,
	})

	if err != nil {
		panic("invalid multisig parameters")
	}
	fromAddr, _ := ma.Address()
	txn, err := transaction.MakePaymentTxn(
		fromAddr.String(),
		"47YPQTIGQEO7T4Y4RWDYWEKV6RTR2UNBQXBABEEGM72ESWDQNCQ52OPASU",
		10,     // fee per byte
		10000,  // amount
		100000, // first valid round
		101000, // last valid round
		nil,    // note
		"",     // closeRemainderTo
		"",     // genesisID
	)

	txid, txBytes, err := crypto.SignMultisigTransaction(addr1.PrivateKey, ma, txn)
	if err != nil {
		println(err.Error)
		panic("could not sign multisig transaction")
	}
	fmt.Printf("Made partially-signed multisig transaction with TxID %s: %x\n", txid, txBytes)
	ioutil.WriteFile("./arbitrary_file.tx", txBytes, 0644)
	readTxBytes, _ := ioutil.ReadFile("./arbitrary_file.tx")
	txid, twoOfThreeTxBytes, err := crypto.AppendMultisigTransaction(addr2.PrivateKey, ma, readTxBytes)
	if err != nil {
		panic("could not append signature to multisig transaction")
	}
	fmt.Printf("Appended bytes %x\n", twoOfThreeTxBytes)
}
