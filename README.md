 # Algorand Hackathon

This repository contains content for participants of hackathons or workshops. **Getting Started with Algorand** and **Algorand 2.0 Workshop** steps are below.


## Getting Started with Algorand


1. Navigate to https://developer.algorand.org/ and click on [Start Building / Workspace Setup](https://developer.algorand.org/docs/build-apps/setup/). Complete the three Start Building tasks: 

* Workspace Setup 
* Connect to Node 
* Your First Transaction 

For workshops, it is recommended to use the [Algorand  Sandbox](https://github.com/algorand/sandbox). It is the quickest way to get up and running with SDK coding.

For development, try to first run your own node, which will install all development tools including goal, kmd, and algokey. However, it will take several hours to sync. 

 

![Dev  portal](/imgs/Hackathon-00.png)

**Figure 1-1** Start Building at developer.algorand.org

2.	For the workshops, clone https://github.com/algorand-devrel/hackathon and follow instructions in the workshop.


# Algorand 2.0 Workshop for Java

The fastest and recommended way to get started is to use the [Algorand Sandbox](https://github.com/algorand/sandbox). 

To debug code samples, clone repository and then navigate to the algorandsamples folder using  Finder or Explorer. Open the _java.code-workspace_ with VS Code. The code is located in the _java-test_ folder. 

**Note:** Want to learn how to debug with VS Code? See our quick tutorial for [Debugging Java](https://github.com/algorand-devrel/hackathon/blob/master/VSCode/Java/vscode.md).

## 1. [Java Workshop – Algorand Standard Assets (ASA)](https://github.com/algorand-devrel/hackathon/blob/master/Tutorials/Java/ASA.md)

Estimated time = 70 minutes

[CLICK HERE TO OPEN WORkSHOP](https://github.com/algorand-devrel/hackathon/blob/master/Tutorials/Java/ASA.md)

In this workshop, you will learn how to build Java SDK solutions for Algorand Standard Assets. 

You will create standalone Accounts, use tools such as [AlgoExplorer](https://algoexplorer.io/) and TestNet Dispenser, as well as use REST API calls.

Additional resources can be found here:

* [Explore Creating Standalone Accounts](https://developer.algorand.org/docs/features/accounts/create/#standalone)

* [Completed code for Creating Accounts](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialCreateAccounts.java)

* Populate the accounts with TestNet Algos using the [TestNet dispenser](https://bank.testnet.algorand.network/)

* [Explore ASA feature](https://developer.algorand.org/docs/features/asa/)

* [Completed ASA WorkShop code](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialAssetExample.java)

## 2. Atomic Transfers

Estimated time: 10 Minutes

* Explore feature: https://developer.algorand.org/docs/features/atomic_transfers/

* Completed code: [_TutorialGroupedTransaction.java_](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialGroupedTransaction.java)

## 3. Algorand Smart Contracts Layer 1

Estimated time: 10 minutes

* Explore feature: https://developer.algorand.org/docs/features/asc1/sdks/

* Completed code: [_TutorialSubmitTealContract.java_](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialSubmitTealContract.java) and [_TutorialSubmitTealDelegate.java_](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialSubmitTealDelegate.java)

![Dev  portal](/imgs/Hackathon-01.png)

**Figure 1** Tutorial Completed code




