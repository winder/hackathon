**LAB Exercise - Algorand Hackathon Developer**

**Table of Contents**

[Getting Started for Hackathon 2](#getting-started-for-hackathon)

[Install Algorand node 2](#_Toc17979065)

[Verify your data directory 6](#_Toc17979066)

[Apply current updates 6](#apply-current-updates)

[Download/Clone Algorand SDKs in your language of choice.
7](#_Toc17979068)

[Install VS Code or alternate IDE (optional)
8](#install-vs-code-or-alternate-ide-optional)

[Unzip the hackathon samples 10](#_Toc17979070)

[Start two terminal sessions 12](#start-two-terminal-sessions)

[Replace tokens and addresses in the sample code.
12](#replace-tokens-and-addresses-in-the-sample-code.)

[Install Postman or your favorite code-agnostic tool for REST API
testing (Paw... etc) (Optional) 14](#_Toc17979073)

[Goal Command Line tools / AlgoExplorer.io
17](#goal-command-line-tools-algoexplorer.io)

[Goal 17](#goal)

[AlgoExplorer.io 20](#algoexplorer.io)

[Getting started with Private Network (optional)
22](#getting-started-with-private-network-optional)

[SDKs 26](#sdks)

[JavaScript SDK 26](#bookmark=id.111kx3o)

[JavaScript SDK Sample webapp 27](#javascript-sdk-sample-webapp)

[Encode/Decode Note Field 33](#encodedecode-note-field)

[Node Example: Retrieving Latest Block Information
34](#node-example-retrieving-latest-block-information)

[More Examples 35](#more-examples)

[Go SDK 35](#bookmark=id.3ygebqi)

[kmdclient.go - kmd client go 39](#kmdclient.go---kmd-client-go)

[backupwallet.go - Backing up a Wallet
40](#backupwallet.go---backing-up-a-wallet)

[signsubmit.go - Signing and submitting a transaction
40](#signsubmit.go---signing-and-submitting-a-transaction)

[signoffline.go - Sign a transaction offline
40](#signoffline.go---sign-a-transaction-offline)

[submittransfilefrom.go - Submit the transaction from a file
40](#submittransfilefrom.go---submit-the-transaction-from-a-file)

[manipulatemultisig.go - Manipulating multisig transactions
40](#manipulatemultisig.go---manipulating-multisig-transactions)

[Java SDK 40](#java-sdk)

[GetBlock.java -- gets the status and lastround
41](#getblock.java-gets-the-status-and-lastround)

[AccountTest.java - Generate account and backup phrase
42](#accounttest.java---generate-account-and-backup-phrase)

[NewWallet.java - kmd client 42](#newwallet.java---kmd-client)

[BackupWallet.java and RestoreWallet.java - Backing up and restoring a
Wallet
42](#backupwallet.java-and-restorewallet.java---backing-up-and-restoring-a-wallet)

[SignAndSubmit.java - Signing and submitting a transaction
43](#signandsubmit.java---signing-and-submitting-a-transaction)

[SignOffline.java - Sign a transaction offline
43](#signoffline.java---sign-a-transaction-offline)

[SubmitFromFile.java - Submit the transaction from a file
43](#submitfromfile.java---submit-the-transaction-from-a-file)

[GetAccountTransactions.java - Get account transactions
43](#getaccounttransactions.java---get-account-transactions)

[Multisig.Java - Manipulating multisig transactions
43](#multisig.java---manipulating-multisig-transactions)

[EncodeDecode.Java - Encode/decode Note Field This sample shows how to
encode and decode the Note Field to build Layer 2 solutions.
43](#encodedecode.java---encodedecode-note-field-this-sample-shows-how-to-encode-and-decode-the-note-field-to-build-layer-2-solutions.)

[Python SDK 43](#python-sdk)

[example.py 43](#example.py)

[Working with the Note Field: 47](#working-with-the-note-field)

[Resources 48](#resources)

**Getting Started for Hackathon**
=================================

**\
Time Estimate: 1 hour**

This section provides guidance on installing an Algorand node and the
tools that will be useful in your hackathon efforts. Happy coding!
Download algorandsamples.zip at
<http://github.com/algorand-devrel/hackathon> and unzip into a folder
off of your \$HOME folder. To get your Home folder location, in terminal
type in:\
\
echo \$HOME

[]{#_Toc17979065 .anchor}**Install Algorand node\
**

Time Estimate 10 minutes

In this section, we will install an Algorand node.

A synchronized node will be provided for this hackathon, however, you
need to install your own node, so you can do this lab exercise and
continue to work on the solution after the hackathon is over, as well as
build other solutions. Follow the instructions on how to install your
node are here:

[<https://developer.algorand.org/docs/introduction-installing-node>\
]{.underline}

By default, an Algorand installation is configured to run on MainNet.
For most users, this is the desired outcome.  Developers, however, need
access to our TestNet or DevNet networks. This
[[guide]{.underline}](https://developer.algorand.org/docs/switching-networks)
will walk you through how to switch networks, if you have not already
done so.

Your node may take a while to sync (several hours). You can proceed to
the following steps in the meantime, noting if the goal command does not
seem to be working, like transferring funds for example, it may be
because the node is not synced yet. To see if it is synced use this
command from terminal:

> goal node status -d \~/node/data

The data directory is the data directory for the node. It may be simpler
to set ALGORAND\_DATA env variable rather than specifying each time. In
that case, the -d should be removed above.

export ALGORAND\_DATA=\~/node/data

If your solution will need to search transactions and blocks, such as
the sample Chess Game here:
<https://github.com/algorand-devrel/chessexample>, then you will need to
change a couple of settings in the config.json file in the data folder,
setting Archival to true, and IsIndexerArchive to true as shown below.
Rename config.json.example to config.json if needed.

![A screenshot of a social media post Description automatically
generated](images/media/image1.png){width="2.6734470691163605in"
height="5.096773840769904in"}

Note: When installing with DEB or RPM packages the binaries will be
installed in the /usr/bin and the data directory will be set to
/var/lib/algorand. It is advisable in these installs that you add the
following export to your shell config files.

export ALGORAND\_DATA=/var/lib/algorand

> When the Sync Time is zero consistently, it will be close to, if not
> all the way, synced.\
> \
> ![A screenshot of a cell phone Description automatically
> generated](images/media/image2.png){width="6.273153980752406in"
> height="1.1125459317585302in"}

**Alternatives**

The above process will take several hours to sync, so there are two
alternatives that can be used in the meantime. Each option provides an
Algod token and a Server URL. These values will be needed in your
solution code as well as the sample hackathon lab exercises.\
\
\
**Option 1 -Algorand Hackathon**

The API Token and Server address can be used in the Hackathon. Once the
hackathon is over, you will need to use your own node or one from
Purestake (see Option 2).

> API Token
>
> ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1\
> \
> Server Address

<http://hackathon.algodev.network:9100/>

The above service is set to Full Archival Indexer Nodes.

**Option 2 - Purestake Token**

PureStake is offering up a token for this hackathon. You do not need to
register. This is free for use during the hackathon.

PureStake\'s token declaration works with the Algorand JavaScript SDK
with a minor update to client instantiation in return for many benefits.

Additional SDK support is in progress.

***Examples of a PureStake GET and POST***

-   [GET
    versions](https://github.com/PureStake/api-examples/blob/master/javascript-examples/get_versions.js)

-   [POST
    transaction](https://github.com/PureStake/api-examples/blob/master/javascript-examples/submit_tx.js)

-   [GET Transaction By
    Id](https://github.com/PureStake/api-examples/blob/master/javascript-examples/get_tx.js)

After the Hackathon is over, you can register to receive a new token
here:

<https://www.purestake.com/algorand-api>

More details here:

<https://www.purestake.com/technology/algorand-services/>

\*\*\*API-Key token for both TestNet and MainNet:

B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab

TestNet Server URL
address: <https://testnet-algorand.api.purestake.io/ps1>

MainNet Server URL
address: <https://mainnet-algorand.api.purestake.io/ps1>

Purestake offers many benefits including a Basic free tier as well as
Pro and Enterprise levels:

Instant Access to Algorand API in Testnet and Mainnet

-   No wait for downloads and blockchain sync times

API is backed by Full Archival Transaction Indexer Nodes

-   Full algod API with performant transaction queries

Secure and Reliable Infrastructure

-   Automated Highly Available Infrastructure, Managed 24x7x365

Secure communications

-   All API traffic over https only

[]{#_Toc17979066 .anchor}**Verify your data directory**\
\
To verify where your data directory is and that you are running TestNet,
use these two commands:

A.  ps aux \| grep algod

> ![A close up of a mans face Description automatically
> generated](images/media/image3.png){width="6.041666666666667in"
> height="0.4166666666666667in"}

B.  goal node status -d \~/node/data

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image4.png){width="5.736111111111111in"
> height="1.0416666666666667in"}

Apply current updates
---------------------

> To manually update use these commands\
> \
> cd \~/node
>
> ./update.sh -d \~/node/data
>
> To configure Auto-Update see:\
> <https://developer.algorand.org/docs/configure-auto-update>

[]{#_Toc17979068 .anchor}**Download/Clone Algorand SDKs in your language
of choice.\
\
These are the SDKs available to date. More are on the way. \*\***\
Time Estimate - 10 minutes

-   Go, see install notes here
    [[https://github.com/algorand/go-algorand-sdk]{.underline}](https://github.com/algorand/go-algorand-sdk)

    -   See Go SDK documentation here:
        [[https://godoc.org/github.com/algorand/go-algorand-sdk]{.underline}](https://godoc.org/github.com/algorand/go-algorand-sdk)

-   Python, see install notes here
    [[https://github.com/algorand/py-algorand-sdk]{.underline}](https://github.com/algorand/py-algorand-sdk)

    -   See Python SDK documentation here:
        [[https://py-algorand-sdk.readthedocs.io/en/latest/]{.underline}](https://py-algorand-sdk.readthedocs.io/en/latest/)

-   Java, see install notes here
    [[https://github.com/algorand/java-algorand-sdk]{.underline}](https://github.com/algorand/java-algorand-sdk)

    -   See Java documentation here
        [<https://algorand.github.io/java-algorand-sdk/> ]{.underline}

<!-- -->

-   JavaScript, see install notes here
    [[https://github.com/algorand/js-algorand-sdk]{.underline}](https://github.com/algorand/js-algorand-sdk)

    -   See JavaScript SDK documentation
        visit [[https://developer.algorand.org]{.underline}](https://developer.algorand.org/)

> \*\* If you do not see your language of choice, we have two swagger
> files that you can load in at
> [[https://app.swaggerhub.com]{.underline}](https://app.swaggerhub.com/)
> to generate your own client. The swagger definition json files are for
> Algod and kmd.
>
> KMD handles all interaction with spending keys, including signing
> transactions. Signing can be stand alone as well.
>
> Algod is responsible for processing the protocol and interacting with
> SQLite to record the ledger. Implements REST API for read only APIs.
>
> These swagger.json files can also be loaded into code agnostic tools
> such as [[Postman]{.underline}](https://www.getpostman.com/downloads/)
> or [[Paw]{.underline}](https://paw.cloud/) for REST API testing. To
> generate the latest swagger definitions, use the following commands...
>
> For **Alogd** use:
>
> [[http://localhost:8080/swagger.json]{.underline}](http://localhost:8080/swagger.json)\
> or
>
> curl http://\$(cat \~/node/data/algod.net)/swagger.json \>
> swagger.json\
> (this will appear in your node folder)
>
> If ALGORAND\_DATA is used, then replace with
>
> curl http://\$(cat \$ALGORAND\_DATA/algod.net)/swagger.json \>
> swagger.json
>
> For **kmd** use:
>
> [[http://localhost:7833/swagger.json]{.underline}](http://localhost:7833/swagger.json)
>
> or
>
> curl http://\$(cat \~/node/data/kmd-v0.5/kmd.net)/swagger.json \>
> swaggerkmd.json
>
> (this will appear in your node folder)
>
> If ALGORAND\_DATA is used, then replace with
>
> curl http://\$(cat \$ALGORAND\_DATA/kmd-v0.5/kmd.net)/swagger.json \>
> swaggerkmd.json
>
> Then import at
> [[https://app.swaggerhub.com]{.underline}](https://app.swaggerhub.com/)
> and select CodeGen Options for the Client SDK in the desired language
>
> ![A screenshot of a cell phone Description automatically
> generated](images/media/image5.png){width="3.028380358705162in"
> height="3.465135608048994in"}

Install VS Code or alternate IDE (optional)
--------------------------------------------

**Install an Integrated Development Environment for coding and debugging
JavaScript, Java, Go and/or Python solutions (optional)**

Time Estimate - 10 minutes

> There are many IDEs to debug many languages. This lab exercise uses
> [[Visual Studio Code on the
> Mac](https://docs.microsoft.com/en-us/visualstudio/mac/?view=vsmac-2019)
> (and many other platforms)]{.underline}. If you are familiar with
> Visual Studio, many of the keyboard shortcuts also work in VS Code
> too. Install the extensions for each language and VS Code facilitates:

-   Debugging

-   Intellisense: tool for facilitating code editing such as code
    > completion, parameter info, quick info, and member lists

-   Workspace support to easily load and run each demo

> Search on each language extension one at a time and install in VS Code
> .

-   Go

-   Python

-   Java

-   JavaScript

Should you decide to use VS Code, your extensions list should look
something like this:

> ![A screenshot of a computer Description automatically
> generated](images/media/image6.png){width="5.26111220472441in"
> height="6.520744750656168in"}

[]{#_Toc17979070 .anchor}**Unzip the hackathon samples**\
\
\
Time Estimate - 5 minutes\
\
From <http://github.com/algorand-devrel/hackathon> Download/Clone the
Hackathon repository which has the algorandsamples.zip file and this
document in the readme.md, Unzip into a folder off of your \$HOME folder

To get your Home folder location, in terminal type in

echo \$HOME

The contents should look similar to this:

![A screenshot of text Description automatically
generated](images/media/image7.png){width="5.329861111111111in"
height="9.0in"}**\
**

Start two terminal sessions
----------------------------

-   In the first one, start localhost using
    [[http-server]{.underline}](https://www.npmjs.com/package/http-server):
    Navigate to algorandsamples folder with finder and right click to
    start terminal session in that folder. Then enter:

> http-server

-   In the other terminal session, navigate to \~/node and start up kmd
    and node (kmd may time out after a while, so don't just assume it is
    running.)

> goal node start -d \~/node/data
>
> goal kmd start -d \~/node/data
>
> If \$ALGORAND\_DATA has been set, simply use
>
> goal node start
>
> goal kmd start

Replace tokens and addresses in the sample code.
-------------------------------------------------

Time Estimate - 10 minutes

Follow the SDK install directions for each in the readme files. In all
of the examples, at some point, you will need to replace the code
placeholders with the Algod and Kmd tokens and network addresses in
each. The files below will only appear if the KMD and Node have been
started above. For the node you created, these can be found here...

> From your node directory copy off values from your data directory or
> \$ALGORAND\_DATA

-   algod.token

-   algod.net

> From your node directory copy off from your data directory or
> \$ALGORAND\_DATA

-   kmd.token

-   kmd.net

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image8.png){width="5.253311461067367in"
> height="5.2280533683289585in"}

For example, in the JavaScript SDK sample webapp test.js file update the
constants with these values:

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image9.png){width="6.5in"
> height="3.3819444444444446in"}

[]{#_Toc17979073 .anchor}**Install Postman or your favorite
code-agnostic tool for REST API testing (Paw... etc) (Optional)\
\
**Time Estimate - 10 minutes

When having a team of hackers, it might be useful to communicate API
discussions with a code agnostic tool, as team members may have
different programming skill sets.

1.  [[https://www.getpostman.com/]{.underline}](https://www.getpostman.com/)

2.  To get the latest Algorand swagger.json files use these commands
    from terminal:

> goal node start -d \~/node/data

goal kmd start -d \~/node/data

curl http://\$(cat \~/node/data/algod.net)/swagger.json \> swagger.json

> curl http://\$(cat \~/node/data/kmd-v0.5/kmd.net)/swagger.json \>
> swaggerkmd.json

3.  If you are using the standup instance for the hackathon use this
    command:

> curl http://\$(cat
> <http://hackathon.algodev.network:9100/swagger.json>)/swagger.json \>
> swagger.json

4.  If you are using the Purestake service you must supply the API token
    in the header and this can be done in Postman\
    \
    ![A screenshot of a social media post Description automatically
    generated](images/media/image10.png){width="4.348386920384952in"
    height="2.4868503937007875in"}

5.  Import these Json swagger files into Postman

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image11.png){width="4.181470909886264in"
> height="1.5086351706036745in"}

6.  Make these changes to the request:

<!-- -->

1.  Click on GET current status node command.

2.  Change http://localhost to
    [[http://127.0.0.1:8080]{.underline}](http://127.0.0.1:8080) for
    algod\
    and [[http://127.0.0.1:7833]{.underline}](http://127.0.0.1:7833) for
    kmd\
    \
    If using the hackathon instance change http://localhost to:
    <http://hackathon.algodev.network:9100/>\
    \
    If using Purestake change http://localhost to:\
    <https://testnet.algo-api.purestake.io/ps1>\
    and add the header\
    Key name and token value provided:\
    ![A screenshot of a cell phone Description automatically
    generated](images/media/image12.png){width="5.241935695538058in"
    height="1.1811154855643045in"}

3.  Add header key and value ([[info
    here]{.underline}](https://developer.algorand.org/docs/using-sdks-and-rest-apis))\
    The API Token for the algod process is named X-Algo-API-Token and
    the kmd\'s is named X-KMD-API-Token.

4.  Press Send to send the request

5.  See the response

> ![A screenshot of a social media post Description automatically
> generated](images/media/image13.png){width="5.523233814523184in"
> height="3.0035542432195976in"}

6.  It might be more convenient to set the variable baseUrl in an
    environment of PostMan rather than changing each request
    individually.

7.  There is a trick to use a pre-request script: right click on the
    collection Algod REST API, edit, then pre-request script, and
    write:\
    pm.request.headers.upsert({key: \'X-Algo-API-Token\', value:
    \'e5c941507a5ae016a47a59a76b492dded26dbf0462283f2a227dae340f11b6ed\'
    })\
    \
    This removes the need of adding the header in all requests.\
    \
    ![A screenshot of a cell phone Description automatically
    generated](images/media/image14.png){width="4.519354768153981in"
    height="1.6884809711286088in"}

**Goal Command Line tools / AlgoExplorer.io**
==============================================

Time Estimate - 10 minutes

In this section we will show how to use the goal command line tool as
well as the AlgoExplorer.io.

Goal
----

The goal command line tool provides access to these objects, methods and
properties.

> ![A screenshot of a computer Description automatically
> generated](images/media/image15.png){width="5.981357174103237in"
> height="3.353650481189851in"}

The data directory is the data directory for the node. It may be simpler
to set ALGORAND\_DATA env variable rather than specifying each time. In
that case, the -d should be removed above.

export ALGORAND\_DATA=\~/node/data

Note: When installing with DEB or RPM packages the binaries will be
installed in the /usr/bin and the data directory will be set to
/var/lib/algorand. It is advisable in these installs that you add the
following export to your shell config files.

export ALGORAND\_DATA=/var/lib/algorand

Also, if you have not set the PATH variable to include the node install
folder, do so with this:

export PATH=\$PATH:\~/node

Restart the terminal session after defining the environment variable

1.  To start a node use this command:\
    \
    goal node start

2.  To start kmd use this command:\
    \
    goal kmd start

3.  To get help with goal use this command:\
    \
    goal -h\
    \
    ![A screenshot of a social media post Description automatically
    generated](images/media/image16.png){width="5.642495625546807in"
    height="2.1376377952755905in"}

4.  To get help with and command

> goal \[command\] -h\
> \
> For example, us this command to get help with node
>
> goal node -h
>
> ![A screenshot of a cell phone Description automatically
> generated](images/media/image17.png){width="5.751596675415573in"
> height="1.9866360454943133in"}

5.  Run this to get a status. We will copy off the block number, for
    subsequent use in AlgoExplorer:

> goal node status

6.  Copy last committed block

![A screenshot of a cell phone Description automatically
generated](images/media/image18.png){width="6.271076115485564in"
height="1.1135181539807524in"}

AlgoExplorer.io
---------------

The AlgoExplorer can be used to search on blocks, transactions and
account addresses in either MainNet or TestNet.

1.  Browse to [[
    https://algoexplorer.io/](https://algoexplorer.io/)]{.underline}

2.  Click on TestNet in the dropdown

![A screenshot of a cell phone Description automatically
generated](images/media/image19.png){width="1.6111111111111112in"
height="1.0694444444444444in"}

3.  Paste in block, press search, click on transactions. If you do not
    see any transactions, use this block: 868921

![A screenshot of a cell phone Description automatically
generated](images/media/image20.png){width="6.5in"
height="4.115277777777778in"}

4.  Click on TxID

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image21.png){width="5.051784776902887in"
> height="3.1703193350831147in"}

5.  Drill into a transaction and notice Sender / receiver / note /
    status / amount / fees and other fields.

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image22.png){width="5.455331364829396in"
> height="4.293742344706912in"}

**Getting started with Private Network (optional)**
===================================================

5 Minutes

In this section you will learn how to create a Private Network. A
Private Network is for developers. It allows you to learn Algorand
Blockchain without having to touch either TestNet or MainNet. Code using
the Algorand SDKs, can be used to access the Private Network.\
\
This exercise is suggested for developers that are just getting started
with Algorand.

Note: the [[Algorand TestNet
Dispenser]{.underline}](https://bank.testnet.algorand.network/), a tool
used to send Algos to an account, only works for TestNet and not a
Private Network.

1)  Navigate to the algorandsamples folder

2)  Open networktemplate.json in VS Code and review. Note the Primary
    and Node Nodes that will be created.

> ![A close up of a map Description automatically
> generated](images/media/image23.png){width="3.506868985126859in"
> height="5.840832239720035in"}

3)  In Finder, navigate to your root directory and show the directory
    structure does not have folders for Node and Primary.

4)  Run this command in Terminal from the node folder to create the
    private network.

> **goal network create -r \~/net1 -n private -t networktemplate.json**

![A screenshot of a cell phone Description automatically
generated](images/media/image24.png){width="5.819444444444445in"
height="0.875in"}

> **goal network start -r \~/net1**
>
> **goal network status -r \~/net1**

![A screenshot of a social media post Description automatically
generated](images/media/image25.png){width="5.638888888888889in"
height="2.263888888888889in"}

5)  Open Finder and look at the directory structure for /net1/Node and
    /net1/Primary\
    \
    ![A screenshot of a cell phone Description automatically
    generated](images/media/image26.png){width="4.388888888888889in"
    height="2.2222222222222223in"}

6)  (Optional) The full set of Private Network tutorials are here:
    [[https://developer.algorand.org/docs/tutorials]{.underline}](https://developer.algorand.org/docs/tutorials).
    This is a great learning resource.

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image27.png){width="5.717777777777778in"
> height="3.858888888888889in"}

Go to
[<https://developer.algorand.org/docs/creating-new-account-and-participation-key>]{.underline}
to complete all the rest of the tasks in this tutorial including:

-   Creating a new wallet

-   Creating a new Account and Participation key

-   Write a raw transaction and post to algod REST server

-   Creating a multisig account

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image28.png){width="2.544479440069991in"
> height="2.563538932633421in"}

7)  Once finished you can delete the private network as this is for
    learning only and cannot be used for further development, other than
    testing with SDK code. Delete the Private Network

> **goal network delete -r \~/net1**

**SDKs**
========

> **\
> Click to go to your desired SDK\
> **
>
> [[JavaScript SDK](#bookmark=id.111kx3o)\
> ]{.underline}

[[Go SDK](#bookmark=id.3ygebqi)\
]{.underline}

> [[Java SDK]{.underline}](#java-sdk)
>
> [[Python SDK]{.underline}](#python-sdk)
>
> []{#bookmark=id.111kx3o .anchor}**JavaScript SDK\
> **Time Estimate - 20 minutes
>
> In this section, we use the sample web app that comes with the
> Algorand JavaScript SDK.

1)  For the JavaScript samples, open folder for myjsdemo into VS Code or
    you favorite IDE\
    \
    Or just open the js.code-workspace in the algorandsamples folder
    with VS Code.

2)  The JavaScript launch.json file should be similar to this

![A screenshot of a social media post Description automatically
generated](images/media/image29.png){width="6.027871828521435in"
height="2.8812707786526683in"}

### JavaScript SDK Sample webapp

3)  JavaScript AlgoSDK is a JavaScript library for communicating with
    the Algorand network for modern browsers and node.js. Navigate to
    the Algorand JavaScript GitHub repository to clone or download and
    note to the examples/webapp folder. This is the code we will use to
    learn the JavaScript SDK from.

[[https://github.com/algorand/js-algorand-sdk]{.underline}](https://github.com/algorand/js-algorand-sdk)

![A screenshot of a cell phone Description automatically
generated](images/media/image30.png){width="6.5in"
height="4.279166666666667in"}

4)  Install the JavaScript SDK using the instructions on the readme
    file.

5)  You make need to update the algosdk.min.js that is included in the
    demo zip, from the SDK clone/zip. But, if you do update, use the
    sample code on GitHub to stay in sync.

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image31.png){width="5.135295275590551in"
> height="2.0541174540682414in"}

6)  Run thru debugger with localhost (launch.json file below) or just
    bring up Finder and double click on Test.html.

7)  Your launch.json file should look similar to this if running
    localhost:

> ![A screenshot of a social media post Description automatically
> generated](images/media/image29.png){width="6.101767279090113in"
> height="2.916593394575678in"}

8)  **Run sample by navigating to the test.html page and click on
    buttons in this order:**

<!-- -->

A.  Get Latest Block

B.  Generate Account

C.  Get Account Details - note the amount as 0, need to add money --
    > copy off account

D.  TestNet Dispenser -- paste account

E.  Recover Account from Mnemonic

F.  Get Account Details -- note the amount (it may take a few seconds to
    > show up)

G.  Submit a transaction (send money)

H.  Get Tx From Account (TX ID) (this may take 4 or 5 seconds to show
    > up)

Screen shots:

A.  Get Latest Block

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image32.png){width="6.079391951006124in"
> height="3.3573042432195974in"}

B.  Generate Account

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image33.png){width="6.026701662292213in"
> height="1.984433508311461in"}

C.  Get Account Details

> ![A screenshot of a social media post Description automatically
> generated](images/media/image34.png){width="6.5in"
> height="2.1631944444444446in"}

D.  Algorand TestNet Dispenser

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image35.png){width="6.5in"
> height="1.2048611111111112in"}

E.  Recover Account from Account Mnemonic

F.  If amount does not show up -- press Get Account Details after a few
    seconds

> ![A screenshot of a social media post Description automatically
> generated](images/media/image36.png){width="6.5in"
> height="2.064583333333333in"}

G.  Submit Transaction

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image37.png){width="6.5in"
> height="1.632638888888889in"}

H.  Get Tx From Account

> ![A screenshot of a cell phone Description automatically
> generated](images/media/image38.png){width="6.5in"
> height="1.7006944444444445in"}

9)  **Walk thru code in code in** **test.js** and **see each function we
    just used**.\
    \
    All of this functionality is accomplished in a little over 200 lines
    of code! How about that!" (Applause) ☺

<!-- -->

A.  Open test.js

B.  Note atoken

C.  Note kmdtoken

D.  If running localhost, see debug console output after scrolling thru
    code, this should be the same as seen when running the demo.

![A screenshot of a social media post Description automatically
generated](images/media/image39.png){width="6.404780183727034in"
height="4.587354549431321in"}

### Encode/Decode Note Field

10) Two other function to note in the code are for encoding and decoding
    the free form Note Field. This field is used to create Layer 2
    solutions. Encode it on submit transaction and decode it when
    needed.

<!-- -->

A)  Open test.js

B)  Locate the submit transaction code

C)  Look for the encodeObj method

![A screenshot of a social media post Description automatically
generated](images/media/image40.png){width="6.5in"
height="5.370833333333334in"}

A)  Open test.js

B)  Locate the get transaction note code

C)  Look for the decodeObj method

![A screenshot of a cell phone Description automatically
generated](images/media/image41.png){width="6.5in" height="4.0375in"}

### Node Example: Retrieving Latest Block Information

11) To retrieve the latest block's information using Node, you would use
    the algod client wrapper functions shown below.

const algosdk = require(\'algosdk\');

//Retrieve the token, server and port values for your installation in
the algod.net

//and algod.token files within the data directory

const atoken = \"your token here\";

const aserver = \"http://127.0.0.1\";

const aport = 8080;

const algodclient = new algosdk.Algod(atoken, aserver, aport);

(async () =\> {

let lastround = (await algodclient.status()).lastRound;

let block = (await algodclient.block(lastround));

console.log( block );

})().catch(e =\> {

console.log(e);

});

### More Examples

12) See
    [[https://developer.algorand.org/docs/javascript-sdk]{.underline}](https://developer.algorand.org/docs/javascript-sdk)
    for more samples.

> [[Web App: Client Wrapper
> Functions]{.underline}](https://developer.algorand.org/docs/javascript-sdk#webapp-client)
>
> [[Node: Retrieving Latest Block
> Information]{.underline}](https://developer.algorand.org/docs/javascript-sdk#node-example-latest)
>
> [[Node: Creating a New Wallet and Account Using
> kmd]{.underline}](https://developer.algorand.org/docs/javascript-sdk#node-example-create-wallet)
>
> [[Node: Backing Up and Restoring a
> Wallet]{.underline}](https://developer.algorand.org/docs/javascript-sdk#node-example-backup-wallet)
>
> [[Node: Signing and Submitting a
> Transaction]{.underline}](https://developer.algorand.org/docs/javascript-sdk#node-example-sign)
>
> [[Node: Locating a
> Transaction]{.underline}](https://developer.algorand.org/docs/javascript-sdk#node-example-find-transaction)
>
> [[Node: Writing to the Note Field of a
> Transaction]{.underline}](https://developer.algorand.org/docs/javascript-sdk#node-example-note-write)
>
> [[Node: Reading the Note Field of a
> Transaction]{.underline}](https://developer.algorand.org/docs/javascript-sdk#node-example-note-read)
>
> [[Node: Creating a Multisignature Account and Signing a
> Transaction]{.underline}](https://developer.algorand.org/docs/javascript-sdk#node-example-multisig)

13) [**[Skip to next section]{.underline}**](#_heading=h.28h4qwu)

> []{#bookmark=id.3ygebqi .anchor}**Go SDK\
> **Time Estimate - 20 minutes

1)  **If using GO, Add the algorandsamples (or the directory you
    unzipped the hackathon code samples to) to the GOPATH environment
    variable**

Add these two line by editing your bash\_profile and then restart your
IDE and terminal sessions:\
\
GOPATH=\~/go:\~/algorandsamples/mygodemo/

export GOPATH

2)  To edit the .bash\_profile to add the above statements, quit
    and restart terminal and Visual Studio Code. Adapt accordingly if
    you are not using bash as a shell. This file is located in your home
    directory and you may need to type in command + shift + . to see it
    in Finder or opening the file in your editor, as it is hidden. 

3)  **For the Go samples, open folder for mygodemo into VS Code or you
    favorite IDE\
    **\
    Or just open the go.code-workspace in the algorandsamples folder
    with VS Code. **\
    **

4)  The Go **launch.json** file should be similar to this:

![A screenshot of a social media post Description automatically
generated](images/media/image42.png){width="5.891573709536308in"
height="3.119068241469816in"}

5)  In this step we will create an algodclient using the Go SDK. We will
    create the algod client and fetch node status information, and then
    a specific block. Optional Go samples are listed below as well.

6)  In the algorandsamples folder Open go.code-workspace or the mygocode
    folder in VS Code

If you have not already done so, Download/clone the Go SDK from:

[[https://github.com/algorand/go-algorand-sdk]{.underline}](https://github.com/algorand/go-algorand-sdk)

7)  **Review the readme file:**

![A screenshot of a social media post Description automatically
generated](images/media/image43.png){width="4.46450021872266in"
height="6.003227252843395in"}

8)  **In VS Code, open algodclient.go in the VS Code Explorer**

![A screenshot of a cell phone Description automatically
generated](images/media/image44.png){width="1.9722222222222223in"
height="4.958333333333333in"}

9)  Your launch.json file should look similar to this:

![A screenshot of a social media post Description automatically
generated](images/media/image45.png){width="5.660655074365704in"
height="2.977891513560805in"}

A.  Select the debugger

B.  Note algodToken

C.  Note MakeClient

D.  Note Status

E.  Run (you may be prompted to open launch.json. After it opens, then
    you need to go back and select algodclient.go again in the Explorer)

F.  Note status info in the debug console

![A screenshot of a cell phone Description automatically
generated](images/media/image46.png){width="6.136952099737533in"
height="4.574520997375328in"}

10) The debug console should show results for creating an algod client,
    algod status and block information.

11) Run as many of the following samples as desired, in this order
    (there is a file for each in a folder in the algosamples/mygodemo
    folder):

### kmdclient.go - kmd client go

The following example creates a wallet and generates an account within
that wallet. This account can now be used to sign transactions, but you
will need some funds to get started. If you are on the test network,
TestNet, you can use
the [[dispenser](https://bank.testnet.algorand.network/) ]{.underline}to
seed your account with some Algos.

### backupwallet.go - Backing up a Wallet

You can export a master derivation key from the wallet and convert it to
a mnemonic phrase in order to back up any generated addresses. This
backup phrase will only allow you to recover wallet-generated keys; if
you import an external key into a kmd-managed wallet, you\'ll need to
back up that key by itself in order to recover it.

To restore a wallet, convert the phrase to a key and pass it
to CreateWallet. This call will fail if the wallet already exists:

### signsubmit.go - Signing and submitting a transaction

The following example shows how to to use both KMD and Algod when
signing and submitting a transaction. You can also sign a transaction
offline, which is shown in the next section of this document.

### signoffline.go - Sign a transaction offline

The following example shows how to create a transaction and sign it
offline. You can also create the transaction online and then sign it
offline.

### submittransfilefrom.go - Submit the transaction from a file

This example takes the output from the previous example (file containing
signed transaction) and submits it to Algod process of a node.

### manipulatemultisig.go - Manipulating multisig transactions

Here, we first create a simple multisig payment transaction, with three
public identities and a threshold of 2.

[**[Skip to next section]{.underline}**](#resources)

Java SDK
--------

Time estimate: 20 minutes

1)  **For the Java samples open folder for java-test into VS Code or you
    favorite IDE\
    **\
    Or just open the java.code-workspace in the algorandsamples folder
    with VS Code.

2)  The Java **launch.json** file should be similar to this:

![A screenshot of a cell phone Description automatically
generated](images/media/image47.png){width="6.5in"
height="2.636111111111111in"}

3)  In this section we will get a block and display the info using the
    Java SDK. We will create the algod client and fetch node status
    information of the latest block. (Other code functions are
    optional).

4)  Show the Go SDK at:

> [[https://github.com/algorand/go-algorand-sdk]{.underline}](https://github.com/algorand/go-algorand-sdk)

### GetBlock.java -- gets the status and lastround

A.  Open the GetBlock.java file

B.  Note the call to getStatus

C.  Note the call to getLastRound

D.  Run

E.  Note the Output Console Display of the latest block

![A screenshot of a social media post Description automatically
generated](images/media/image48.png){width="5.70200678040245in"
height="4.420273403324584in"}

5)  Run as many of the following scripts as desired, in this order
    (there is a file for each in a folder):

### AccountTest.java - Generate account and backup phrase

This example creates a random account, a backup phrase and performs a
recovery

This account can now be used to sign transactions, but you will need
some funds to get started. If you are on the test network, TestNet, you
can use
the [[dispenser](https://bank.testnet.algorand.network/) ]{.underline}to
seed your account with some Algos.

### NewWallet.java - kmd client

The following example creates a wallet and generates an account within
that wallet. This account can now be used to sign transactions, but you
will need some funds to get started. If you are on the test network,
TestNet, you can use
the [[dispenser](https://bank.testnet.algorand.network/) ]{.underline}to
seed your account with some Algos.

### BackupWallet.java and RestoreWallet.java - Backing up and restoring a Wallet

You can export a master derivation key from the wallet and convert it to
a mnemonic phrase in order to back up any generated addresses. This
backup phrase will only allow you to recover wallet-generated keys; if
you import an external key into a kmd-managed wallet, you\'ll need to
back up that key by itself in order to recover it.

To restore a wallet, convert the phrase to a key and pass it
to CreateWallet. This call will fail if the wallet already exists:

### SignAndSubmit.java - Signing and submitting a transaction

The following example shows how to use both KMD and Algod when signing
and submitting a transaction. You can also sign a transaction offline,
which is shown in the next section of this document.

### SignOffline.java - Sign a transaction offline

The following example shows how to create a transaction and sign it
offline. You can also create the transaction online and then sign it
offline.

### SubmitFromFile.java - Submit the transaction from a file

This example takes the output from the previous example (file containing
signed transaction) and submits it to Algod process of a node.

### GetAccountTransactions.java - Get account transactions

This example gets transactions for an account.

###  Multisig.Java - Manipulating multisig transactions

Here, we first create a simple multisig payment transaction, with three
public identities and a threshold of 2.

### EncodeDecode.Java - Encode/decode Note Field  This sample shows how to encode and decode the Note Field to build Layer 2 solutions.

[**[Skip to next section]{.underline}**](#resources)

 Python SDK
----------

Time estimate: 20 minutes

###  example.py

1)  **For the Python samples open folder for mypythondemo into VS Code
    or you favorite IDE\
    **\
    Or just open the python.code-workspace in the algorandsamples folder
    with VS Code.

2)  The Python **launch.json** file should be similar to this:

![A screenshot of a social media post Description automatically
generated](images/media/image49.png){width="6.5in"
height="3.0993055555555555in"}

3)  If not already done, clone or download the Python SDK at:

> [[https://github.com/algorand/py-algorand-sdk]{.underline}](https://github.com/algorand/py-algorand-sdk)

4)  Use the instructions on the readme file to install the SDK

5)  In this section we will run the example.py code from the SDK. Before
    running example.py start kmd and alogd using these goal commands:\
    \
    goal kmd start -d \[data directory\]\
    \
    goal node start -d \[data directory\]

6)  Next, create a wallet and account, and copy off account address.

goal wallet new \[wallet name\] -d \[data directory\]\
\
goal account new -d \[data directory\] -w \[wallet name\]

7)  Paste the account address into the [Algorand [TestNet
    D]{.underline}ispenser](https://bank.testnet.algorand.network/) to
    send Algos to this account.

8)  Edit params.py and add token information and data-dir-path\
    \
    1) Edit params.py\
    2) Add token info for algod and kmd\
    3) Add your data directory path

![A screenshot of a cell phone Description automatically
generated](images/media/image50.png){width="6.5in"
height="2.9715277777777778in"}

9)  Edit example.py\
    \
    1) Edit example.py\
    2) Uncomment Enter your Wallet, password and account info\
    3) Comment prompt for these values\
    4) Run the code and see the results in the Output console

![A screenshot of a cell phone Description automatically
generated](images/media/image51.png){width="5.733589238845144in"
height="4.04720363079615in"}

10) The example code performs the following functions:\
    \
    Create kmd and algod clients

> Create a new kmd wallet
>
> Generate an account and import to wallet
>
> Get the mnemonic
>
> Get last block
>
> Create a transaction
>
> Sign a transaction with kmd
>
> Sign a transaction with account
>
> Send the transaction

To see the new wallet and accounts we created use:

goal wallet list -w \[wallet name\] -d data

You should see something like this:

![A close up of a device Description automatically
generated](images/media/image52.png){width="6.5in"
height="1.6277777777777778in"}

goal account list -w \[wallet name\] -d data

You should see something like this:

![A close up of a device Description automatically
generated](images/media/image53.png){width="6.5in"
height="0.45069444444444445in"}

Other examples -- (optional)

Using the Wallet class
[[https://github.com/algorand/py-algorand-sdk\#using-the-wallet-class]{.underline}](https://github.com/algorand/py-algorand-sdk#using-the-wallet-class)

Backing up a wallet with mnemonic
[[https://github.com/algorand/py-algorand-sdk\#backing-up-a-wallet-with-mnemonic]{.underline}](https://github.com/algorand/py-algorand-sdk#backing-up-a-wallet-with-mnemonic)

Recovering a wallet using a backup phrase
[[https://github.com/algorand/py-algorand-sdk\#recovering-a-wallet-using-a-backup-phrase]{.underline}](https://github.com/algorand/py-algorand-sdk#recovering-a-wallet-using-a-backup-phrase)

Writing transactions to file
[[https://github.com/algorand/py-algorand-sdk\#writing-transactions-to-file]{.underline}](https://github.com/algorand/py-algorand-sdk#writing-transactions-to-file)

Manipulating multisig transactions
[[https://github.com/algorand/py-algorand-sdk\#manipulating-multisig-transactions]{.underline}](https://github.com/algorand/py-algorand-sdk#manipulating-multisig-transactions)

### Working with the Note Field:

[[https://github.com/algorand/py-algorand-sdk\#working-with-notefield]{.underline}](https://github.com/algorand/py-algorand-sdk#working-with-notefield)



[**[Skip to next section]{.underline}**](#resources)

**\
**

**Resources**
=============

**More resources can be found here:**

AlgoExplorer.io

-   [[https://algoexplorer.io/]{.underline}](https://algoexplorer.io/)

Algorand GitHub

-   [[https://github.com/algorand]{.underline}](https://github.com/algorand)

Algorand TestNet Dispenser

-   [[https://bank.testnet.algorand.network/]{.underline}](https://bank.testnet.algorand.network/)

Developer Portal

-   [[https://developer.algorand.org/]{.underline}](https://developer.algorand.org/docs/developer-faq)

Developer FAQs

-   [[https://developer.algorand.org/docs/developer-faq]{.underline}](https://developer.algorand.org/docs/developer-faq)

Forums

-   [[https://forum.algorand.org/]{.underline}](https://forum.algorand.org/)

Community Portal -- Events, Blog, Chapters, etc

-   [[https://community.algorand.org/]{.underline}](https://community.algorand.org/)

Community Ambassador program

-   [[https://community.algorand.org/ambassadors/]{.underline}](https://community.algorand.org/ambassadors/)

Swagger hub

-   [[https://swagger.io/tools/swaggerhub/]{.underline}](https://swagger.io/tools/swaggerhub/)

Algorand Foundation Roadmap

-   [[https://algorand.foundation/roadmap]{.underline}](https://algorand.foundation/roadmap)

Token Dynamics

-   [[https://algorand.foundation/token-dynamics]{.underline}](https://algorand.foundation/token-dynamics)

YouTube Algorand

-   [[https://www.youtube.com/algorand]{.underline}](https://www.youtube.com/algorand)

Consensus 2019 videos - Turing award winner - Silvio Micali keynote - is
in the second group (Construct) \#55 - Building the Technical Innovation
Required for a New Borderless Economy

-   [[https://www.coindesk.com/events/consensus-2019/videos]{.underline}](https://www.coindesk.com/events/consensus-2019/videos)

![A screenshot of a social media post Description automatically
generated](images/media/image54.png){width="6.5in"
height="3.9451388888888888in"}

More resources here:

-   Go
    [[https://golang.org/doc/install]{.underline}](https://golang.org/doc/install).

-   Python
    [[www.python.org/downloads]{.underline}](http://www.python.org/downloads)

-   Maven\
    [[http://www.codebind.com/mac-osx/install-maven-mac-os/]{.underline}](http://www.codebind.com/mac-osx/install-maven-mac-os/)

> or
>
> [[https://www.baeldung.com/install-maven-on-windows-linux-mac]{.underline}](https://www.baeldung.com/install-maven-on-windows-linux-mac)

-   Java\
    [[https://www.oracle.com/technetwork/java/javase/downloads/index.html]{.underline}](https://www.oracle.com/technetwork/java/javase/downloads/index.html)

-   Enable JavaScript in browsers
    [[https://www.techwalla.com/articles/how-to-enable-javascript-on-a-mac]{.underline}](https://www.techwalla.com/articles/how-to-enable-javascript-on-a-mac)

-   Install Node JS for localhost server
    [[https://www.npmjs.com/package/http-server]{.underline}](https://www.npmjs.com/package/http-server)
