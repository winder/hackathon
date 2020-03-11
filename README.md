 # Algorand Hackathon

This repository contains content for participants of hackathons or workshops. **Getting Started with Algorand** and **Algorand 2.0 Workshop** steps are below.


## Getting Started with Algorand


1. Navigate to https://developer.algorand.org/ and click on [Start Building / Workspace Setup](https://developer.algorand.org/docs/build-apps/setup/). Complete the three Start Building tasks: 

* Workspace Setup 
* Connect to Node 
* Your First Transaction 

It is recommended to first run your own node, which will install all development tools including goal, kmd, and algokey. However, it will take several hours to sync. While your node syncs, you may want to use the Docker Sandbox or a Third-party service as documented. Not listed on the setup page is another option which uses a hackathon workshop instance and this is documented below. 

![Dev  portal](/imgs/Hackathon-00.png)

**Figure 1-1** Start Building at developer.algorand.org

2.	For the workshop, download the completed code from https://github.com/algorand-devrel/hackathon. The code includes a hackathon token and server provided in the file _algorandsamples.zip_. 
This code runs with little or no modifications for JavaScript, Java, Go and Python.
The sample code includes a hackathon token and server URL in the file _algorandsamples.zip_. This code runs with no modifications for JavaScript, Java, Go and Python. Sample C# code can be found [here](https://github.com/RileyGe/dotnet-algorand-sdk)


## Algorand 2.0 Workshop for Java

The fastest and recommended way to start using a node for SDK coding is to use the [Sandbox](https://github.com/algorand/sandbox). All of the completed code below runs without any modifications. By default, the code uses the sandbox endpoints.

Sandbox uses the following API endpoints:

address: http://localhost:4001

token: aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

 The code is in [algroandsamples java-test folder](https://github.com/algorand-devrel/hackathon/tree/master/algorandsamples/java-test/src/main/java/com/algorand/javatest) 
 
 Another option to the sandbox endpoints is to use a stand-up hackathon instance endpoints ... so these endpoints can be used if a sandbox has not been setup:

* address: http://hackathon.algodev.network:9100

* token: "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"

To debug code samples, navigate to the algorandsamples folder in finder or explorer and open the java.code-workspace with VS Code. The code is located in the java-test folder. 

**Note:** Want to learn how to debug with VS Code? See our quick tutorial for [Debugging Java](https://github.com/algorand-devrel/hackathon/blob/master/VSCode/Java/vscode.md).

**1. Create Standalone Accounts**

* Explore feature: https://developer.algorand.org/docs/features/accounts/create/#standalone

* Completed code: [_TutorialCreateAccounts.java_](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialCreateAccounts.java)

**2. Dispense funds**

* Populate the accounts with TestNet Algos using the [dispenser](https://bank.testnet.algorand.network/)

**3. Algorand Standard Assets**

* Explore feature: <https://developer.algorand.org/docs/features/asa/>

* Completed code: [_TutorialAssetExample.java_](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialAssetExample.java)

**4. Atomic Transfers**

* Explore feature: https://developer.algorand.org/docs/features/atomic_transfers/

* Completed code: [_TutorialGroupedTransaction.java_](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialGroupedTransaction.java)

**5. Algorand Smart Contracts Layer 1**

* Explore feature: https://developer.algorand.org/docs/features/asc1/sdks/

* Completed code: [_TutorialSubmitTealContract.java_](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialSubmitTealContract.java) and [_TutorialSubmitTealDelegate.java_](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialSubmitTealDelegate.java)

![Dev  portal](/imgs/Hackathon-01.png)

**Figure 1-2** Tutorial Completed code




