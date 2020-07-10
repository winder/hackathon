//Here, we first create a simple multisig payment transaction, with three public identities and a threshold of 2:

addr1, _ := types.DecodeAddress("DN7MBMCL5JQ3PFUQS7TMX5AH4EEKOBJVDUF4TCV6WERATKFLQF4MQUPZTA")
addr2, _ := types.DecodeAddress("BFRTECKTOOE7A5LHCF3TTEOH2A7BW46IYT2SX5VP6ANKEXHZYJY77SJTVM")
addr3, _ := types.DecodeAddress("47YPQTIGQEO7T4Y4RWDYWEKV6RTR2UNBQXBABEEGM72ESWDQNCQ52OPASU")
ma, err := crypto.MultisigAccountWithParams(1, 2, []types.Address{
	addr1,
	addr2,
	addr3,
})
if err != nil {
	panic("invalid multisig parameters")
}
fromAddr, _ := ma.Address()
txn, err := transaction.MakePaymentTxn(
	fromAddr.String(),
	"INSERTTOADDRESHERE",
	10,     // fee per byte
	10000,  // amount
	100000, // first valid round
	101000, // last valid round
	nil,    // note
	"",     // closeRemainderTo
	"",     // genesisID
)
txid, txBytes, err := crypto.SignMultisigTransaction(secretKey, ma, txn)
if err != nil {
	panic("could not sign multisig transaction")
}
fmt.Printf("Made partially-signed multisig transaction with TxID %s: %x\n", txid, txBytes)
//Now, we can write the returned bytes to disk:

_ := ioutil.WriteFile("./arbitrary_file.tx", txBytes, 0644)
//And read them back in:

readTxBytes, _ := ioutil.ReadFile("./arbitrary_file.tx")
//Now, we can append another signature, to hit the threshold. Note that this SDK forces new signers to know the parameters of the multisig - after all, we don't want to sign things without knowing the identity of the multi-signature.

// as before
addr1, _ := types.DecodeAddress("DN7MBMCL5JQ3PFUQS7TMX5AH4EEKOBJVDUF4TCV6WERATKFLQF4MQUPZTA")
addr2, _ := types.DecodeAddress("BFRTECKTOOE7A5LHCF3TTEOH2A7BW46IYT2SX5VP6ANKEXHZYJY77SJTVM")
addr3, _ := types.DecodeAddress("47YPQTIGQEO7T4Y4RWDYWEKV6RTR2UNBQXBABEEGM72ESWDQNCQ52OPASU")
ma, _ := crypto.MultisigAccountWithParams(1, 2, []types.Address{
	addr1,
	addr2,
	addr3,
})
// append our signature to readTxBytes
txid, twoOfThreeTxBytes, err := crypto.AppendMultisigTransaction(secretKey, ma, readTxBytes)
if err != nil {
	panic("could not append signature to multisig transaction")
}
fmt.Printf("Made 2-out-of-3 multisig transaction with TxID %s: %x\n", txid, twoOfThreeTxBytes)
//We can also merge raw, partially-signed multisig transactions:

otherTxBytes := ... // generate another raw multisig transaction somehow
txid, mergedTxBytes, err := crypto.MergeMultisigTransactions(twoOfThreeTxBytes, otherTxBytes)