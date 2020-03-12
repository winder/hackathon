## 1. Create 3 Accounts and add Algos to the Accounts

Time estimate - 10 minutes

Assets are created at the account level. So, before starting the ASA workshop, 3 new accounts will be created for this step for ASA transactions. Once created, copy off the account mnemonic and account address values.

**Task 1-1** Setup. Clone the [Hackathon Repository](https://github.com/algorand-devrel/hackathon).

**Task 1-2** Open the workspace or folder. You will need to start a new instance of VS Code for this workshop. Navigate to the algorandsamples folder using Finder/Explorer. 

Right click on the file _java.code-workspace_ and select Open With ... and select Visual Studio Code or add it by selecting Other... If using another IDE or opening from VS Code, open the completed source code folder  algorandsamples/java-test folder. 

![Figure Step 1-2 Open WorkSpace from Finder/Explorer](/imgs/TutorialASA-03.png)
**Figure Step 1-2** Open WorkSpace from Finder/Explorer.

**Task 1-3** Create your code. Create an empty code file in the source folder (/algorandsamples/java-test/src/main/java/com/algorand/javatest) and name it  _myTutorialCreateAccounts.java_

Simply copy and insert this snippet below and run it. This code will generate an account address and recovery mnemonic phrase for 3 accounts. Then copy off the account addresses and mnemonics from the output window. We will use these in the next task. Mnemonics are for demonstration purposes. **NEVER** reveal secret mnemonics in practice as these are used to derive the secret (or private) key.

```java
package com.algorand.javatest;
import com.algorand.algosdk.account.Account;

public class myTutorialCreateAccounts {
    public static void main(String args[]) throws Exception {
        Account myAccount1 = new Account();
        System.out.println("My Address1: " + myAccount1.getAddress());
        System.out.println("My Passphrase1: " + myAccount1.toMnemonic());
        Account myAccount2 = new Account();
        System.out.println("My Address2: " + myAccount2.getAddress());
        System.out.println("My Passphrase2: " + myAccount2.toMnemonic());
        Account myAccount3 = new Account();
        System.out.println("My Address3: " + myAccount3.getAddress());
        System.out.println("My Passphrase3: " + myAccount3.toMnemonic());

        //Copy off accounts and mnemonics    
        //Dispense TestNet Algos to each account: https://bank.testnet.algorand.network/
    }
}
```
**Task 1-4** 
In order to run transactions, the accounts need to have TestNet Algo funds to cover transaction fees. Load the accounts from the Algorand [TestNet Dispenser](https://bank.testnet.algorand.network/) 

Completed code here:

[Create Accounts](https://github.com/algorand-devrel/hackathon/blob/master/algorandsamples/java-test/src/main/java/com/algorand/javatest/TutorialCreateAccounts.java)