LAB Exercise - Algorand Hackathon Developer<br>
===============================================

Getting Started for Hackathon
-----------------------------

**Time Estimate: 1 hour**

This section provides guidance on installing an Algorand node and the tools that
will be useful in your hackathon efforts. Happy coding! Download
algorandsamples.zip at <http://github.com/algorand-devrel/hackathon> and unzip
into a folder off of your \$HOME folder. To get your Home folder location, in
terminal type in:  
  
echo \$HOME

### Install Algorand node

Time Estimate 10 minutes

In this section, we will install an Algorand node.

A synchronized node will be provided for this hackathon, however, you need to
install your own node, so you can do this lab exercise and continue to work on
the solution after the hackathon is over, as well as build other solutions.
Follow the instructions on how to install your node are here:

<https://developer.algorand.org/docs/introduction-installing-node>  


By default, an Algorand installation is configured to run on MainNet. For most
users, this is the desired outcome.  Developers, however, need access to our
TestNet or DevNet networks. This
[guide](https://developer.algorand.org/docs/switching-networks) will walk you
through how to switch networks, if you have not already done so.

Your node may take a while to sync (several hours). You can proceed to the
following steps in the meantime, noting if the goal command does not seem to be
working, like transferring funds for example, it may be because the node is not
synced yet. To see if it is synced use this command from terminal:

>   goal node status -d \~/node/data

The data directory is the data directory for the node. It may be simpler to set
ALGORAND_DATA env variable rather than specifying each time. In that case, the
-d should be removed above.

export ALGORAND_DATA=\~/node/data

If your solution will need to search transactions and blocks, such as the sample
Chess Game here: <https://github.com/algorand-devrel/chessexample>, then you
will need to change a couple of settings in the config.json file in the data
folder, setting Archival to true, and IsIndexerArchive to true as shown below.
Rename config.json.example to config.json if needed.

![A screenshot of a social media post Description automatically generated](media/34ad69b32a6b583719be78db5b3aceb6.png)

Note: When installing with DEB or RPM packages the binaries will be installed in
the /usr/bin and the data directory will be set to /var/lib/algorand. It is
advisable in these installs that you add the following export to your shell
config files.

export ALGORAND_DATA=/var/lib/algorand

>   When the Sync Time is zero consistently, it will be close to, if not all the
>   way, synced.  
>     
>   

![](media/f069f5ed68dca75813a8e56c25bfd4d0.png)

>   A screenshot of a cell phone Description automatically generated

### Alternatives

The above process will take several hours to sync, so there are two alternatives
that can be used in the meantime. Each option provides an Algod token and a
Server URL. These values will be needed in your solution code as well as the
sample hackathon lab exercises.  
  
  
Option 1 -Algorand Hackathon

The API Token and Server address can be used in the Hackathon. Once the
hackathon is over, you will need to use your own node or one from Purestake (see
Option 2).

>   API Token

>   ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1  
>     
>   Server Address

<http://hackathon.algodev.network:9100/>

The above service is set to Full Archival Indexer Nodes.

### Option 2 – Purestake Token

PureStake is offering up a token for this hackathon. You do not need to
register. This is free for use during the hackathon.

PureStake's token declaration works with the Algorand JavaScript SDK with a
minor update to client instantiation in return for many benefits.

Additional SDK support is in progress.

### Examples of a PureStake GET and POST

-   [GET
    versions](https://github.com/PureStake/api-examples/blob/master/javascript-examples/get_versions.js)

-   [POST
    transaction](https://github.com/PureStake/api-examples/blob/master/javascript-examples/submit_tx.js)

-   [GET Transaction By
    Id](https://github.com/PureStake/api-examples/blob/master/javascript-examples/get_tx.js)

After the Hackathon is over, you can register to receive a new token here:

<https://www.purestake.com/algorand-api>

More details here:

<https://www.purestake.com/technology/algorand-services/>

\*\*\*API-Key token for both TestNet and MainNet:

B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab

TestNet Server URL address: <https://testnet-algorand.api.purestake.io/ps1>

MainNet Server URL address: <https://mainnet-algorand.api.purestake.io/ps1>

Purestake offers many benefits including a Basic free tier as well as Pro and
Enterprise levels:

Instant Access to Algorand API in Testnet and Mainnet

-   No wait for downloads and blockchain sync times

API is backed by Full Archival Transaction Indexer Nodes

-   Full algod API with performant transaction queries

Secure and Reliable Infrastructure

-   Automated Highly Available Infrastructure, Managed 24x7x365

Secure communications

-   All API traffic over https only

Verify your data directory  
  
To verify where your data directory is and that you are running TestNet, use
these two commands:

1.  ps aux \| grep algod

![](media/aaf48b01d07ea34bee41b5b8c027b869.png)

>   A close up of a mans face Description automatically generated

1.  goal node status -d \~/node/data

![](media/95d8ea93272f20775b4a1796deed51bb.png)

>   A screenshot of a cell phone Description automatically generated

### Apply current updates

>   To manually update use these commands  
>     
>   cd \~/node

>   ./update.sh -d \~/node/data

>   To configure Auto-Update see:  
>   <https://developer.algorand.org/docs/configure-auto-update>

Download/Clone Algorand SDKs in your language of choice.  
  
**These are the SDKs available to date. More are on the way. \*\***  
Time Estimate - 10 minutes

-   Go, see install notes here <https://github.com/algorand/go-algorand-sdk>

    -   See Go SDK documentation here:
        <https://godoc.org/github.com/algorand/go-algorand-sdk>

-   Python, see install notes here <https://github.com/algorand/py-algorand-sdk>

    -   See Python SDK documentation here:
        <https://py-algorand-sdk.readthedocs.io/en/latest/>

-   Java, see install notes here <https://github.com/algorand/java-algorand-sdk>

    -   See Java documentation here
        <https://algorand.github.io/java-algorand-sdk/>* *

-   JavaScript, see install notes here
    <https://github.com/algorand/js-algorand-sdk>

    -   See JavaScript SDK documentation
        visit [https://developer.algorand.org](https://developer.algorand.org/)

>   \*\* If you do not see your language of choice, we have two swagger files
>   that you can load in at
>   [https://app.swaggerhub.com](https://app.swaggerhub.com/) to generate your
>   own client. The swagger definition json files are for Algod and kmd.

>   KMD handles all interaction with spending keys, including signing
>   transactions. Signing can be stand alone as well.

>   Algod is responsible for processing the protocol and interacting with SQLite
>   to record the ledger. Implements REST API for read only APIs.

>   These swagger.json files can also be loaded into code agnostic tools such as
>   [Postman](https://www.getpostman.com/downloads/) or
>   [Paw](https://paw.cloud/) for REST API testing. To generate the latest
>   swagger definitions, use the following commands…

>   For **Alogd** use:

>   <http://localhost:8080/swagger.json>  
>   or

>   curl http://\$(cat \~/node/data/algod.net)/swagger.json \> swagger.json  
>   (this will appear in your node folder)

>   If ALGORAND_DATA is used, then replace with

>   curl http://\$(cat \$ALGORAND_DATA/algod.net)/swagger.json \> swagger.json

>   For **kmd** use:

>   <http://localhost:7833/swagger.json>

>   or

>   curl http://\$(cat \~/node/data/kmd-v0.5/kmd.net)/swagger.json \>
>   swaggerkmd.json

>   (this will appear in your node folder)

>   If ALGORAND_DATA is used, then replace with

>   curl http://\$(cat \$ALGORAND_DATA/kmd-v0.5/kmd.net)/swagger.json \>
>   swaggerkmd.json

>   Then import at [https://app.swaggerhub.com](https://app.swaggerhub.com/) and
>   select CodeGen Options for the Client SDK in the desired language

![](media/1505cc7aca00a190a4aee10c98bc88d8.png)

>   A screenshot of a cell phone Description automatically generated

### Install VS Code or alternate IDE (optional)<br>

**Install an Integrated Development Environment for coding and debugging
JavaScript, Java, Go and/or Python solutions (optional)**

Time Estimate - 10 minutes

>   There are many IDEs to debug many languages. This lab exercise uses [Visual
>   Studio Code on the
>   Mac](https://docs.microsoft.com/en-us/visualstudio/mac/?view=vsmac-2019)
>   *(and many other platforms)*. If you are familiar with Visual Studio, many
>   of the keyboard shortcuts also work in VS Code too. Install the extensions
>   for each language and VS Code facilitates:

-   Debugging

-   Intellisense: tool for facilitating code editing such as code completion,
    parameter info, quick info, and member lists

-   Workspace support to easily load and run each demo

>   Search on each language extension one at a time and install in VS Code .

-   Go

-   Python

-   Java

-   JavaScript

Should you decide to use VS Code, your extensions list should look something
like this:

![](media/7dc1ca459305e52d093007d28b28412c.png)

>   A screenshot of a computer Description automatically generated

Unzip the hackathon samples  
  
  
Time Estimate - 5 minutes  
  
From <http://github.com/algorand-devrel/hackathon> Download/Clone the Hackathon
repository which has the algorandsamples.zip file and this document in the
readme.md, Unzip into a folder off of your \$HOME folder

To get your Home folder location, in terminal type in

echo \$HOME

The contents should look similar to this:

![A screenshot of text Description automatically generated](media/6bd344124383e44192dc7b593da7be6e.png)

### Start two terminal sessions 

-   In the first one, start localhost using
    [http-server](https://www.npmjs.com/package/http-server): Navigate to
    algorandsamples folder with finder and right click to start terminal session
    in that folder. Then enter:

>   http-server

-   In the other terminal session, navigate to \~/node and start up kmd and node
    (kmd may time out after a while, so don’t just assume it is running.)

>   goal node start -d \~/node/data

>   goal kmd start -d \~/node/data

>   If \$ALGORAND_DATA has been set, simply use

>   goal node start

>   goal kmd start

### Replace tokens and addresses in the sample code. 

Time Estimate - 10 minutes

Follow the SDK install directions for each in the readme files. In all of the
examples, at some point, you will need to replace the code placeholders with the
Algod and Kmd tokens and network addresses in each. The files below will only
appear if the KMD and Node have been started above. For the node you created,
these can be found here…

>   From your node directory copy off values from your data directory or
>   \$ALGORAND_DATA

-   algod.token

-   algod.net

>   From your node directory copy off from your data directory or
>   \$ALGORAND_DATA

-   kmd.token

-   kmd.net

![](media/750726004faef8ea4e0171522637adb0.png)

>   A screenshot of a cell phone Description automatically generated

For example, in the JavaScript SDK sample webapp test.js file update the
constants with these values:

![](media/af2396b732eb0e0eed11b52a799203d0.png)

>   A screenshot of a cell phone Description automatically generated

Install Postman or your favorite code-agnostic tool for REST API testing (Paw…
etc) (Optional)  
  
Time Estimate - 10 minutes

When having a team of hackers, it might be useful to communicate API discussions
with a code agnostic tool, as team members may have different programming skill
sets.

1.  <https://www.getpostman.com/>

2.  To get the latest Algorand swagger.json files use these commands from
    terminal:

>   goal node start -d \~/node/data

goal kmd start -d \~/node/data

curl http://\$(cat \~/node/data/algod.net)/swagger.json \> swagger.json

>   curl http://\$(cat \~/node/data/kmd-v0.5/kmd.net)/swagger.json \>
>   swaggerkmd.json

1.  If you are using the standup instance for the hackathon use this command:

>   curl http://\$(cat
>   <http://hackathon.algodev.network:9100/swagger.json>)/swagger.json \>
>   swagger.json

1.  If you are using the Purestake service, you must supply the API token in the
    header and this can be done in Postman.  
      
    

    ![A screenshot of a social media post Description automatically generated](media/d36a7c1e9ae1be71207bb8409e528734.png)

2.  Import these Json swagger files into Postman

![](media/7e497ed376d280824be06766aeace0a9.png)

>   A screenshot of a cell phone Description automatically generated

1.  Make these changes to the request:

2.  Click on GET current status node command.

>   Change http://localhost to <http://127.0.0.1:8080> for algod  
>   and <http://127.0.0.1:7833> for kmd  
>     
>   If using the hackathon instance change http://localhost to:
>   <http://hackathon.algodev.network:9100/>  
>     
>   If using Purestake change http://localhost to:  
>   <http://testnet.algo-api.purestake.io/ps1>  
>   and the Key name is X-API-Token with Value of
>   B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab

![](media/5307527b32eaa5ad0de7af747aa8f032.png)

>   A screenshot of a cell phone Description automatically generated

1.  If not using Purestake, add header key and value ([info
    here](https://developer.algorand.org/docs/using-sdks-and-rest-apis))  
    The API Token for the algod process key name is X-Algo-API-Token and the
    kmd's Key name is X-KMD-API-Token. If using the hackathon instance, the
    algod X-Algo-API-Token  value is:
    ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1  
    Press Send to send the request

2.  See the response

![](media/8fc17a52b7f1f7f54e0f5075f7ced80d.png)

>   A screenshot of a social media post Description automatically generated

1.  It might be more convenient to set the variable baseUrl in an environment of
    PostMan rather than changing each request individually.

2.  There is a trick to use a pre-request script: right click on the collection
    Algod REST API, edit, then pre-request script, and write:  
    pm.request.headers.upsert({key: 'X-Algo-API-Token', value:
    'e5c941507a5ae016a47a59a76b492dded26dbf0462283f2a227dae340f11b6ed' })  
      
    This removes the need of adding the header in all requests.  
      
    

    ![A screenshot of a cell phone Description automatically generated](media/f123ea768a6e2c5e38d658f9c55fbf74.png)

Goal Command Line tools / AlgoExplorer.io
-----------------------------------------

Time Estimate - 10 minutes

In this section we will show how to use the goal command line tool as well as
the AlgoExplorer.io.

### Goal

The goal command line tool provides access to these objects, methods and
properties.

![](media/939993ce523be2673b9a0a6037a1cd21.png)

>   A screenshot of a computer Description automatically generated

The data directory is the data directory for the node. It may be simpler to set
ALGORAND_DATA env variable rather than specifying each time. In that case, the
-d should be removed above.

export ALGORAND_DATA=\~/node/data

Note: When installing with DEB or RPM packages the binaries will be installed in
the /usr/bin and the data directory will be set to /var/lib/algorand. It is
advisable in these installs that you add the following export to your shell
config files.

export ALGORAND_DATA=/var/lib/algorand

Also, if you have not set the PATH variable to include the node install folder,
do so with this:

export PATH=\$PATH:\~/node

Restart the terminal session after defining the environment variable

1.  To start a node use this command:  
      
    goal node start

2.  To start kmd use this command:  
      
    goal kmd start

3.  To get help with goal use this command:  
      
    goal -h  
      
    

    ![A screenshot of a social media post Description automatically generated](media/957ce5b29917b215e792715102d5aa3c.png)

4.  To get help with and command

>   goal [command] -h  
>     
>   For example, us this command to get help with node

>   goal node -h

![](media/1b3879b40042663ec1adb7f709488359.png)

>   A screenshot of a cell phone Description automatically generated

1.  Run this to get a status. We will copy off the block number, for subsequent
    use in AlgoExplorer:

>   goal node status

1.  Copy last committed block

![A screenshot of a cell phone Description automatically generated](media/55a1e30fb07cf2b71b6feeff62c51788.png)

### AlgoExplorer.io

The AlgoExplorer can be used to search on blocks, transactions and account
addresses in either MainNet or TestNet.

1.  Browse to <https://algoexplorer.io/>

2.  Click on TestNet in the dropdown

![A screenshot of a cell phone Description automatically generated](media/e93b29ee71c42f46cd69572f55f22e1d.png)

1.  Paste in block, press search, click on transactions. If you do not see any
    transactions, use this block: 868921

![A screenshot of a cell phone Description automatically generated](media/f854ffab00b620699b20f8205b125c0b.png)

1.  Click on TxID

![](media/ee185a60301f2d5669a75f615830c9d3.png)

>   A screenshot of a cell phone Description automatically generated

1.  Drill into a transaction and notice Sender / receiver / note / status /
    amount / fees and other fields.

![](media/ea9e0dac347cb553324dc2f03619b8df.png)

>   A screenshot of a cell phone Description automatically generated

SDKs
----

>     
>   **Click to go to your desired SDK**  
>   

>   [JavaScript SDK](#javascript-sdk)  
>   

[Go SDK](#go-sdk)  


>   [Java SDK](#java-sdk)

>   [Python SDK](#python-sdk)

**JavaScript SDK**
------------------

>     
>   Time Estimate - 20 minutes

>   In this section, we use the sample web app that comes with the Algorand
>   JavaScript SDK.

1.  For the JavaScript samples, open folder for myjsdemo into VS Code or you
    favorite IDE  
      
    Or just open the js.code-workspace in the algorandsamples folder with VS
    Code.

2.  The JavaScript launch.json file should be similar to this

![A screenshot of a social media post Description automatically generated](media/2f7af7a9ec7900adb5a7489ef162fdb1.png)

### JavaScript SDK Sample webapp

1.  JavaScript AlgoSDK is a JavaScript library for communicating with the
    Algorand network for modern browsers and node.js. Navigate to the Algorand
    JavaScript GitHub repository to clone or download and note to the
    examples/webapp folder. This is the code we will use to learn the JavaScript
    SDK from.

<https://github.com/algorand/js-algorand-sdk>

![A screenshot of a cell phone Description automatically generated](media/aa054eef12ebc2e27a5dbbeff92e0fc9.png)

1.  Install the JavaScript SDK using the instructions on the readme file.

2.  You make need to update the algosdk.min.js that is included in the demo zip,
    from the SDK clone/zip. But, if you do update, use the sample code on GitHub
    to stay in sync.

![](media/d85b87fe6f822cdb77ae18fe801f89bf.png)

>   A screenshot of a cell phone Description automatically generated

1.  Run thru debugger with localhost (launch.json file below) or just bring up
    Finder and double click on Test.html.

2.  Your launch.json file should look similar to this if running localhost:

![](media/2f7af7a9ec7900adb5a7489ef162fdb1.png)

>   A screenshot of a social media post Description automatically generated

1.  **Run sample by navigating to the test.html page and click on buttons in
    this order:**

2.  Get Latest Block

3.  Generate Account

4.  Get Account Details - note the amount as 0, need to add money – copy off
    account

5.  TestNet Dispenser – paste account

6.  Recover Account from Mnemonic

7.  Get Account Details – note the amount (it may take a few seconds to show up)

8.  Submit a transaction (send money)

9.  Get Tx From Account (TX ID) (this may take 4 or 5 seconds to show up)

Screen shots:

1.  Get Latest Block

![](media/94cad5841cdbf79cd67f3f142790d548.png)

>   A screenshot of a cell phone Description automatically generated

1.  Generate Account

![](media/7e3ecfbfd429c2a215237c9011ffb533.png)

>   A screenshot of a cell phone Description automatically generated

1.  Get Account Details

![](media/0eedda905477e39cc33223dea06727d2.png)

>   A screenshot of a social media post Description automatically generated

1.  Algorand TestNet Dispenser

![](media/d6746f6f0ad0630cc8690e68962101ac.png)

>   A screenshot of a cell phone Description automatically generated

1.  Recover Account from Account Mnemonic

2.  If amount does not show up – press Get Account Details after a few seconds

![](media/df455bf0017c520ceb089108af09d03a.png)

>   A screenshot of a social media post Description automatically generated

1.  Submit Transaction

![](media/19f891c9d7c2836621d36f22498a24fb.png)

>   A screenshot of a cell phone Description automatically generated

1.  Get Tx From Account

![](media/4d5841c33c0f1170f2e4c6201afd8453.png)

>   A screenshot of a cell phone Description automatically generated

1.  **Walk thru code in code in test.js** and **see each function we just
    used**.  
      
    All of this functionality is accomplished in a little over 200 lines of
    code! How about that!” (Applause) ☺

2.  Open test.js

3.  Note atoken

4.  Note kmdtoken

5.  If running localhost, see debug console output after scrolling thru code,
    this should be the same as seen when running the demo.

![A screenshot of a social media post Description automatically generated](media/94c9695887581625c2d914f21476e921.png)

### Encode/Decode Note Field

1.  Two other function to note in the code are for encoding and decoding the
    free form Note Field. This field is used to create Layer 2 solutions. Encode
    it on submit transaction and decode it when needed.

2.  Open test.js

3.  Locate the submit transaction code

4.  Look for the encodeObj method

![A screenshot of a social media post Description automatically generated](media/b123176520affd845abc28c47cbdf020.png)

1.  Open test.js

2.  Locate the get transaction note code

3.  Look for the decodeObj method

![A screenshot of a cell phone Description automatically generated](media/891b43f4c1742899c63af28d93d5151f.png)

### Node Example: Retrieving Latest Block Information<br>

1.  To retrieve the latest block’s information using Node, you would use the
    algod client wrapper functions shown below.

const algosdk = require('algosdk');

//Retrieve the token, server and port values for your installation in the
algod.net

//and algod.token files within the data directory

const atoken = "your token here";

const aserver = "http://127.0.0.1";

const aport = 8080;

const algodclient = new algosdk.Algod(atoken, aserver, aport);

(async () =\> {

let lastround = (await algodclient.status()).lastRound;

let block = (await algodclient.block(lastround));

console.log( block );

})().catch(e =\> {

console.log(e);

});

### More Examples<br>

1.  See <https://developer.algorand.org/docs/javascript-sdk> for more samples.

>   [Web App: Client Wrapper
>   Functions](https://developer.algorand.org/docs/javascript-sdk#webapp-client)

>   [Node: Retrieving Latest Block
>   Information](https://developer.algorand.org/docs/javascript-sdk#node-example-latest)

>   [Node: Creating a New Wallet and Account Using
>   kmd](https://developer.algorand.org/docs/javascript-sdk#node-example-create-wallet)

>   [Node: Backing Up and Restoring a
>   Wallet](https://developer.algorand.org/docs/javascript-sdk#node-example-backup-wallet)

>   [Node: Signing and Submitting a
>   Transaction](https://developer.algorand.org/docs/javascript-sdk#node-example-sign)

>   [Node: Locating a
>   Transaction](https://developer.algorand.org/docs/javascript-sdk#node-example-find-transaction)

>   [Node: Writing to the Note Field of a
>   Transaction](https://developer.algorand.org/docs/javascript-sdk#node-example-note-write)

>   [Node: Reading the Note Field of a
>   Transaction](https://developer.algorand.org/docs/javascript-sdk#node-example-note-read)

>   [Node: Creating a Multisignature Account and Signing a
>   Transaction](https://developer.algorand.org/docs/javascript-sdk#node-example-multisig)

1.  [Skip to next section](#_heading=h.28h4qwu)

**Go SDK**
----------

>     
>   Time Estimate - 20 minutes

1.  **If using GO, Add the algorandsamples (or the directory you unzipped the
    hackathon code samples to) to the GOPATH environment variable**

Add these two line by editing your bash_profile and then restart your IDE and
terminal sessions:  
  
GOPATH=\~/go:\~/algorandsamples/mygodemo/

export GOPATH

1.  To edit the .bash_profile to add the above statements, quit and restart
    terminal and Visual Studio Code. Adapt accordingly if you are not using bash
    as a shell. This file is located in your home directory and you may need to
    type in command + shift + . to see it in Finder or opening the file in your
    editor, as it is hidden. 

2.  **For the Go samples, open folder for mygodemo into VS Code or you favorite
    IDE**  
      
    Or just open the go.code-workspace in the algorandsamples folder with VS
    Code.  
    

3.  The Go **launch.json** file should be similar to this:

![A screenshot of a social media post Description automatically generated](media/53c9336b015511f11d6d67ff1081dca9.png)

1.  In this step we will create an algodclient using the Go SDK. We will create
    the algod client and fetch node status information, and then a specific
    block. Optional Go samples are listed below as well.

2.  In the algorandsamples folder Open go.code-workspace or the mygocode folder
    in VS Code

If you have not already done so, Download/clone the Go SDK from:

<https://github.com/algorand/go-algorand-sdk>

1.  **Review the readme file:**

![A screenshot of a social media post Description automatically generated](media/534855105d36b6516e3bafb8a988e894.png)

1.  **In VS Code, open algodclient.go in the VS Code Explorer**

![A screenshot of a cell phone Description automatically generated](media/d22ae955f34b046152fc7e918d5e8921.png)

1.  Your launch.json file should look similar to this:

![A screenshot of a social media post Description automatically generated](media/7d69000c07115b996529660d4223c357.png)

1.  Select the debugger

2.  Note algodToken

3.  Note MakeClient

4.  Note Status

5.  Run (you may be prompted to open launch.json. After it opens, then you need
    to go back and select algodclient.go again in the Explorer)

6.  Note status info in the debug console

![A screenshot of a cell phone Description automatically generated](media/03826c49aa6cde67796e7fc33182efc8.png)

1.  The debug console should show results for creating an algod client, algod
    status and block information.

2.  Run as many of the following samples as desired, in this order (there is a
    file for each in a folder in the algosamples/mygodemo folder):

### kmdclient.go - kmd client go

The following example creates a wallet and generates an account within that
wallet. This account can now be used to sign transactions, but you will need
some funds to get started. If you are on the test network, TestNet, you can use
the [dispenser](https://bank.testnet.algorand.network/)* *to seed your account
with some Algos.

### backupwallet.go - Backing up a Wallet 

You can export a master derivation key from the wallet and convert it to a
mnemonic phrase in order to back up any generated addresses. This backup phrase
will only allow you to recover wallet-generated keys; if you import an external
key into a kmd-managed wallet, you'll need to back up that key by itself in
order to recover it.

To restore a wallet, convert the phrase to a key and pass it to CreateWallet.
This call will fail if the wallet already exists:

### signsubmit.go - Signing and submitting a transaction

The following example shows how to to use both KMD and Algod when signing and
submitting a transaction. You can also sign a transaction offline, which is
shown in the next section of this document.

### signoffline.go - Sign a transaction offline

The following example shows how to create a transaction and sign it offline. You
can also create the transaction online and then sign it offline.

### submittransfilefrom.go - Submit the transaction from a file 

This example takes the output from the previous example (file containing signed
transaction) and submits it to Algod process of a node.

### manipulatemultisig.go - Manipulating multisig transactions 

Here, we first create a simple multisig payment transaction, with three public
identities and a threshold of 2.

[Skip to next section](#_resources)

Java SDK
--------

Time estimate: 20 minutes

1.  **For the Java samples open folder for java-test into VS Code or you
    favorite IDE**  
      
    Or just open the java.code-workspace in the algorandsamples folder with VS
    Code.

2.  The Java **launch.json** file should be similar to this:

![A screenshot of a cell phone Description automatically generated](media/99dfd850cfdea1a72400f1338b84ad3a.png)

1.  In this section we will get a block and display the info using the Java SDK.
    We will create the algod client and fetch node status information of the
    latest block. (Other code functions are optional).

2.  Show the Go SDK at:

>   <https://github.com/algorand/go-algorand-sdk>

### GetBlock.java – gets the status and lastround

1.  Open the GetBlock.java file

2.  Note the call to getStatus

3.  Note the call to getLastRound

4.  Run

5.  Note the Output Console Display of the latest block

![A screenshot of a social media post Description automatically generated](media/98a87507aa62ccf94adeabdce992bf1b.png)

1.  Run as many of the following scripts as desired, in this order (there is a
    file for each in a folder):

### AccountTest.java - Generate account and backup phrase 

This example creates a random account, a backup phrase and performs a recovery

This account can now be used to sign transactions, but you will need some funds
to get started. If you are on the test network, TestNet, you can use
the [dispenser](https://bank.testnet.algorand.network/)* *to seed your account
with some Algos.

### NewWallet.java - kmd client 

The following example creates a wallet and generates an account within that
wallet. This account can now be used to sign transactions, but you will need
some funds to get started. If you are on the test network, TestNet, you can use
the [dispenser](https://bank.testnet.algorand.network/)* *to seed your account
with some Algos.

### BackupWallet.java and RestoreWallet.java - Backing up and restoring a Wallet 

You can export a master derivation key from the wallet and convert it to a
mnemonic phrase in order to back up any generated addresses. This backup phrase
will only allow you to recover wallet-generated keys; if you import an external
key into a kmd-managed wallet, you'll need to back up that key by itself in
order to recover it.

To restore a wallet, convert the phrase to a key and pass it to CreateWallet.
This call will fail if the wallet already exists:

### SignAndSubmit.java - Signing and submitting a transaction 

The following example shows how to use both KMD and Algod when signing and
submitting a transaction. You can also sign a transaction offline, which is
shown in the next section of this document.

### SignOffline.java - Sign a transaction offline 

The following example shows how to create a transaction and sign it offline. You
can also create the transaction online and then sign it offline.

### SubmitFromFile.java - Submit the transaction from a file 

This example takes the output from the previous example (file containing signed
transaction) and submits it to Algod process of a node.

### GetAccountTransactions.java - Get account transactions 

This example gets transactions for an account.

###  Multisig.Java - Manipulating multisig transactions

Here, we first create a simple multisig payment transaction, with three public
identities and a threshold of 2.

### EncodeDecode.Java - Encode/decode Note Field<br><br>This sample shows how to encode and decode the Note Field to build Layer 2 solutions.<br>

[Skip to next section](#_resources)

Python SDK
----------

Time estimate: 20 minutes

### <br>example.py

1.  **For the Python samples open folder for mypythondemo into VS Code or you
    favorite IDE**  
      
    Or just open the python.code-workspace in the algorandsamples folder with VS
    Code.

2.  The Python **launch.json** file should be similar to this:

![A screenshot of a social media post Description automatically generated](media/d1fd9d28a75ba9144cfc5c50b0d3096a.png)

1.  If not already done, clone or download the Python SDK at:

>   <https://github.com/algorand/py-algorand-sdk>

1.  Use the instructions on the readme file to install the SDK

2.  In this section we will run the example.py code from the SDK. Before running
    example.py start kmd and alogd using these goal commands:  
      
    goal kmd start -d [data directory]  
      
    goal node start -d [data directory]

3.  Next, create a wallet and account, and copy off account address.

goal wallet new [wallet name] -d [data directory]  
  
goal account new -d [data directory] -w [wallet name]

1.  Paste the account address into the [Algorand TestNet
    Dispenser](https://bank.testnet.algorand.network/) to send Algos to this
    account.

2.  Edit params.py and add token information and data-dir-path  
      
    1) Edit params.py  
    2) Add token info for algod and kmd  
    3) Add your data directory path

![A screenshot of a cell phone Description automatically generated](media/20de068e6cb25493bc36932f5426f006.png)

1.  Edit example.py  
      
    1) Edit example.py  
    2) Uncomment Enter your Wallet, password and account info  
    3) Comment prompt for these values  
    4) Run the code and see the results in the Output console

![A screenshot of a cell phone Description automatically generated](media/c4b23f83277024a9d8e9d62f51acac3b.png)

1.  The example code performs the following functions:  
      
    Create kmd and algod clients

>   Create a new kmd wallet

>   Generate an account and import to wallet

>   Get the mnemonic

>   Get last block

>   Create a transaction

>   Sign a transaction with kmd

>   Sign a transaction with account

>   Send the transaction

To see the new wallet and accounts we created use:

goal wallet list -w [wallet name] -d data

You should see something like this:

![A close up of a device Description automatically generated](media/2ee096a9c3937db02757c10f6a746678.png)

goal account list -w [wallet name] -d data

You should see something like this:

![A close up of a device Description automatically generated](media/00223e60e8a7776b882287b8adef5351.png)

Other examples – (optional)

Using the Wallet class
<https://github.com/algorand/py-algorand-sdk#using-the-wallet-class>

Backing up a wallet with mnemonic
<https://github.com/algorand/py-algorand-sdk#backing-up-a-wallet-with-mnemonic>

Recovering a wallet using a backup phrase
<https://github.com/algorand/py-algorand-sdk#recovering-a-wallet-using-a-backup-phrase>

Writing transactions to file
<https://github.com/algorand/py-algorand-sdk#writing-transactions-to-file>

Manipulating multisig transactions
<https://github.com/algorand/py-algorand-sdk#manipulating-multisig-transactions>

### Working with the Note Field:

<https://github.com/algorand/py-algorand-sdk#working-with-notefield>

[Skip to next section](#_resources)

Getting started with Private Network (optional)
-----------------------------------------------

5 Minutes

In this section you will learn how to create a Private Network. A Private
Network is for developers. It allows you to learn Algorand Blockchain without
having to touch either TestNet or MainNet. Code using the Algorand SDKs, can be
used to access the Private Network.  
  
This exercise is suggested for developers that are just getting started with
Algorand.

Note: the [Algorand TestNet Dispenser](https://bank.testnet.algorand.network/),
a tool used to send Algos to an account, only works for TestNet and not a
Private Network.

1.  Navigate to the algorandsamples folder

2.  Open networktemplate.json in VS Code and review. Note the Primary and Node
    Nodes that will be created.

![](media/fef8c85cfebcf1008ac9873c1cb116de.png)

>   A close up of a map Description automatically generated

1.  In Finder, navigate to your root directory and show the directory structure
    does not have folders for Node and Primary.

2.  Run this command in Terminal from the node folder to create the private
    network.

>   **goal network create -r \~/net1 -n private -t networktemplate.json**

![A screenshot of a cell phone Description automatically generated](media/396ec53e20937e04e1a5e015c1ac7c89.png)

>   **goal network start -r \~/net1**

>   **goal network status -r \~/net1**

![A screenshot of a social media post Description automatically generated](media/37ef77ee2e7d05b41f20dcea57efe721.png)

1.  Open Finder and look at the directory structure for /net1/Node and
    /net1/Primary  
      
    

    ![A screenshot of a cell phone Description automatically generated](media/4dd1801a4835941cb8e41983e01f3235.png)

2.  (Optional) The full set of Private Network tutorials are here:
    <https://developer.algorand.org/docs/tutorials>. This is a great learning
    resource.

![](media/e8757b51bc64d99bf50f3673f6061851.png)

>   A screenshot of a cell phone Description automatically generated

Go to
<https://developer.algorand.org/docs/creating-new-account-and-participation-key>
to complete all the rest of the tasks in this tutorial including:

-   Creating a new wallet

-   Creating a new Account and Participation key

-   Write a raw transaction and post to algod REST server

-   Creating a multisig account

![](media/9c552049b375d85c381826359f52cf05.png)

>   A screenshot of a cell phone Description automatically generated

1.  Once finished you can delete the private network as this is for learning
    only and cannot be used for further development, other than testing with SDK
    code. Delete the Private Network

>   **goal network delete -r \~/net1**

Resources
---------

**More resources can be found here:**

AlgoExplorer.io

-   <https://algoexplorer.io/>

Algorand GitHub

-   <https://github.com/algorand>

Algorand TestNet Dispenser

-   <https://bank.testnet.algorand.network/>

Developer Portal

-   [https://developer.algorand.org/](https://developer.algorand.org/docs/developer-faq)

Developer FAQs

-   <https://developer.algorand.org/docs/developer-faq>

Forums

-   <https://forum.algorand.org/>

Community Portal – Events, Blog, Chapters, etc

-   <https://community.algorand.org/>

Community Ambassador program

-   <https://community.algorand.org/ambassadors/>

Swagger hub

-   <https://swagger.io/tools/swaggerhub/>

Algorand Foundation Roadmap

-   <https://algorand.foundation/roadmap>

Token Dynamics

-   <https://algorand.foundation/token-dynamics>

YouTube Algorand

-   <https://www.youtube.com/algorand>

Consensus 2019 videos - Turing award winner - Silvio Micali keynote - is in the
second group (Construct) \#55 - Building the Technical Innovation Required for a
New Borderless Economy

-   <https://www.coindesk.com/events/consensus-2019/videos>

![A screenshot of a social media post Description automatically generated](media/ac20240a4c83cb36162201c301d44c25.png)

More resources here:

-   Go <https://golang.org/doc/install>.

-   Python [www.python.org/downloads](http://www.python.org/downloads)

-   Maven  
    <http://www.codebind.com/mac-osx/install-maven-mac-os/>

>   or

>   <https://www.baeldung.com/install-maven-on-windows-linux-mac>

-   Java  
    <https://www.oracle.com/technetwork/java/javase/downloads/index.html>

-   Enable JavaScript in browsers
    <https://www.techwalla.com/articles/how-to-enable-javascript-on-a-mac>

-   Install Node JS for localhost server
    <https://www.npmjs.com/package/http-server>
