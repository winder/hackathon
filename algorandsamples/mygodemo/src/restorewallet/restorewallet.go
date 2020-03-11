package main

import (
	"fmt"

	"github.com/algorand/go-algorand-sdk/client/kmd"
	"github.com/algorand/go-algorand-sdk/mnemonic"
	"github.com/algorand/go-algorand-sdk/types"
)

// These constants represent the kmd REST endpoint and the corresponding API
// token. You can retrieve these from the `kmd.net` and `kmd.token` files in
// the kmd data directory.
const kmdAddress = "http://127.0.0.1:7833"
const kmdToken = "your kmdToken"

func main() {
	// Create a kmd client
	// Create a kmd client
	kmdClient, err := kmd.MakeClient(kmdAddress, kmdToken)
	if err != nil {
		fmt.Printf("failed to make kmd client: %s\n", err)
		return
	}
	backupPhrase := "hidden lonely hockey jar drum seat hundred pizza decorate winner ordinary visual into worry winner vivid million humble feed trigger soup share trophy above kite"
	keyBytes, err := mnemonic.ToKey(backupPhrase)
	if err != nil {
		fmt.Printf("failed to get key: %s\n", err)
		return
	}

	var mdk types.MasterDerivationKey
	copy(mdk[:], keyBytes)
	cwResponse, err := kmdClient.CreateWallet("testwallet2", "testpassword", kmd.DefaultWalletDriver, mdk)
	if err != nil {
		fmt.Printf("error creating wallet: %s\n", err)
		return
	}
	fmt.Printf("Created wallet '%s' with ID: %s\n", cwResponse.Wallet.Name, cwResponse.Wallet.ID)
}
