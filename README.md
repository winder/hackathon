# Algorand Hackathon

This repository contains content for participants of hackathons or workshops. **Getting Started with Algorand** and **Algorand 2.0 Workshop** steps are below.


## Getting Started with Algorand


1. Navigate to https://developer.algorand.org/ and click on [Start Building / Workspace Setup](https://developer.algorand.org/docs/build-apps/setup/). It is recommended to first run your own node, which will install all development tools including goal, kmd, and algokey. However, it will take several hours to sync. So, for hackathon or workshop development, you may want to use, while your node syncs, the Docker Sandbox or a Third-party service as documented. Not listed on the setup page is another option, a hackathon workshop instance, and documented below. Then, follow the steps on Connect to Node and Your First Transaction pages.

![Dev  portal](/imgs/Hackathon-00.png)

**Figure 1-1** Start Building at developer.algorand.org

2.	For the workshop, download the completed code from https://github.com/algorand-devrel/hackathon. The code includes a hackathon token and server provided in the file _algorandsamples.zip_. 
This code runs with little or no modifications for JavaScript, Java, Go and Python.
The sample code includes a hackathon token and server URL in the file _algorandsamples.zip_. This code runs with no modifications for JavaScript, Java, Go and Python. Sample C# code can be found [here](https://github.com/RileyGe/dotnet-algorand-sdk)


## Algorand 2.0 Workshop for Java

All of the completed code below runs without any modifcations and is in algroandsamples.zip (/java-test/src/main/java/com/algorand/javatest) and uses a stand-up hackathon instance:

* Token: "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"

* Server: http://hackathon.algodev.network:9100

**Note: ** Want to learn how to debug with VS Code? See our quick tutorial for [Debugging Java](https://github.com/algorand-devrel/hackathon/blob/master/VSCode/Java/vscode.md).

**1. Create Standalone Accounts**

* Explore feature: https://developer.algorand.org/docs/features/accounts/create/#standalone

* Completed code: _TutorialCreateAccounts.java_

**2. Dispense funds**

* Populate the accounts with TestNet Algos using the [dispenser](https://bank.testnet.algorand.network/)

**3. Algorand Standard Assets**

* Explore feature: <https://developer.algorand.org/docs/features/asa/>

* Completed code: _TutorialAssetExample.java_

**4. Atomic Transfers**

* Explore feature: https://developer.algorand.org/docs/features/atomic_transfers/

* Completed code: _TutorialGroupedTransaction.java_

**5. Algorand Smart Contracts Layer 1**

* Explore feature: https://developer.algorand.org/docs/features/asc1/sdks/

* Completed code: _TutorialSubmitTealContract.java_ and _TutorialSubmitTealDelegate.java_

![Dev  portal](/imgs/Hackathon-01.png)

**Figure 1-2** Tutorial Completed code




