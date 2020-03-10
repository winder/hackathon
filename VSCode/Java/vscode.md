
# Using VS Code with Java


# Summary
Being able to debug is a great tool when learning new programming content. This tutorial will facilitate how to debug using Visual Studio (VS) Code, which can be used with all the code samples on the [Algorand Developer Docs Portal](https://developer.algorand.org/docs/). Other debuggers can also be used. If you are new to debugging, VS Code is the most popular tool. It supports many programming languages, is quick to load and has a [wealth of documentation and extensions](https://code.visualstudio.com/Docs).

# Requirements
* Knowledge of setup for a Java solution, including directory structure. 

* The sample in this tutorial requires [Java SDK Installation](https://developer.algorand.org/docs/reference/sdks/#java). 

# Background

Visual Studio Code is a lightweight but powerful source code editor which runs on your desktop and is available for Windows, macOS and Linux. It comes with built-in support for JavaScript, TypeScript and Node.js and has a rich ecosystem of extensions for other languages. VS Code can take on most of the tasks of an Integrated Development Environment (IDE) with the right configuration and plugin library extension. [VS Code is open source](https://github.com/Microsoft/vscode).

Extensions are available in most development languages for VS Code. Specific install instuctions are in Step 2 below. For general information see: [Java](https://code.visualstudio.com/docs/languages/java), [JavaScript](https://code.visualstudio.com/docs/languages/javascript), [Go](https://code.visualstudio.com/docs/languages/go), [Python](https://code.visualstudio.com/docs/languages/python) and [C#](https://code.visualstudio.com/docs/languages/csharp). See extensions [for VS Code languages here ](https://code.visualstudio.com/docs/languages/overview).

For documentation, several other extensions are available including [Grammarly](https://marketplace.visualstudio.com/items?itemName=znck.grammarly) and [MarkDown (MD)](https://code.visualstudio.com/docs/languages/markdown). 

This tutorial walks through a Java example.


## 1. Download VS Code
Download VS Code from this site https://code.visualstudio.com/Download

## 2. Install Extensions

Open VS Code. Go to the Extensions view.
Filter the extensions list by typing your programming language of choice, such as Java,  and you will see all related extensions.

![Filter Extensions](/imgs/VSCodeJava-02.png)

Figure 2-1 Java Extension Pack

**Java:**

To help set up Java on VS Code, search for [Java Extension Pack](https://marketplace.visualstudio.com/items?itemName=vscjava.vscode-java-pack) and install. This contains the most popular extensions for most Java developers:

* Language Support for Java(TM) by Red Hat
* Debugger for Java
* Java Test Runner
* Maven for Java
* Java Dependency Viewer
* Visual Studio IntelliCode



## 3. Debugging with VS Code

For general information on setting up debugging in VS Code see this:
<https://code.visualstudio.com/docs/editor/debugging>

Optionally, for Keyboard shortcuts and Keymap extensions, which match other editors, see this:
<https://code.visualstudio.com/docs/getstarted/keybindings>

## 4. Debugging Java with VS Code sample
 
In this step, we will show how to create a debug session for Java using VS Code. The steps are similar for all languages. Here is a sample JavaScript file to test with. This code generates an Account and retrieves the mnemonic. Name this file GenerateAlgorandKeyPair.java

```Java
package com.algorand.Tutorials;
import com.algorand.algosdk.account.Account;

public class GenerateAlgorandKeyPair {
    public static void main(final String args[]) throws Exception {
        Account myAccount = new Account();
        String myMnemonic = myAccount.toMnemonic();
        System.out.println("My Address: " + myAccount.getAddress());
        System.out.println("My Passphrase: " + myMnemonic);
    }
```
![Press Run Debug Java (F5)](/imgs/VSCodeJava-00.png)

**Figure 4-1** - Press Run Debug Java (F5)

A) Select the Debug icon on the left 

B) Set a breakpoint in the code. To set a breakpoint, simply click on the left margin, sometimes referred to as the gutter, on the line to break.  

C) See the breakpoints set in the debug pane.

D) Press Run and Debug and you should hit your breakpoint. If you do not see a Run and Debug button, this means you already have a launch.json. Open it to see the settings, it is located in the .vscode folder in your code folder. Each configuration should appear in the Debug and Run drop-down menu. 

E) Optionally, to customize Run and Debug, click on "Create a launch.json". Launch.json is the setting file for the debugger. 


Your breakpoint should be hit, and the program execution paused on the line of your breakpoint. It is highlighted.

![Debugging](/imgs/VSCodeJava-01.png)

**Figure 4-2** - Debugging.

We are now debugging! See Figure 4-2 above.

A) Local Variables

B) Hover over a variable with the cursor. See the Data Tip with the contents

C) Navigate by selecting run (to next breakpoint), step over, step into, step out of or stop 



# Learn More 


[Introductory VS Code Videos](https://code.visualstudio.com/docs/getstarted/introvideos)

Keyboard Shortcuts charts for: 
[Windows](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-windows.pdf), 
[MacOS](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-macos.pdf), and 
[Linux](https://code.visualstudio.com/shortcuts/keyboard-shortcuts-macos.pdf)



