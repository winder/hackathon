# Algorand Hackathon Developer Guide

**Table of Contents**
[[toc]]

# **Getting Started for Hackathon**

**Time Estimate: 1 hour**

This section provides guidance on installing an Algorand node and the
tools that will be useful in your hackathon efforts. Happy coding!
Download algorandsamples.zip at
[http://github.com/algorand-devrel/hackathon](http://github.com/algorand-devrel/hackathon) and unzip into a folder
off of your $HOME folder. To get your Home folder location, in terminal
type in:

```
> echo $HOME
```

<span id="_Toc17979065" class="anchor"></span>**Install Algorand node**

Time Estimate 10 minutes

In this section, we will install an Algorand node.

A synchronized node will be provided for this hackathon, however, you
need to install your own node, so you can do this lab exercise and
continue to work on the solution after the hackathon is over, as well as
build other solutions. Follow the instructions on how to install your
node are here:

<span class="underline"><a href="https://developer.algorand.org/docs/introduction-installing-node">https://developer.algorand.org/docs/introduction-installing-node</a></span>

<br>
By default, an Algorand installation is configured to run on MainNet.
For most users, this is the desired outcome.  Developers, however, need
access to our TestNet or DevNet networks. This
[\\<span class="underline"\>guide\\</span\>](https://developer.algorand.org/docs/switching-networks)
will walk you through how to switch networks, if you have not already
done so.

Your node may take a while to sync (several hours). You can proceed to
the following steps in the meantime, noting if the goal command does not
seem to be working, like transferring funds for example, it may be
because the node is not synced yet. To see if it is synced use this
command from terminal:

```
> goal node status -d \~/node/data
```

The data directory is the data directory for the node. It may be simpler
to set ALGORAND_DATA env variable rather than specifying each time. In
that case, the -d should be removed above.

```
export ALGORAND\_DATA=\~/node/data
```

If your solution will need to search transactions and blocks, such as
the sample Chess Game here:
[https://github.com/algorand-devrel/chessexample](https://github.com/algorand-devrel/chessexample), then you will need to
change a couple of settings in the config.json file in the data folder,
setting Archival to true, and IsIndexerArchive to true as shown below.
Rename config.json.example to config.json if needed.

![A screenshot of a social media post Description automatically
generated](images/media/image1.png)

Note: When installing with DEB or RPM packages the binaries will be
installed in the /usr/bin and the data directory will be set to
/var/lib/algorand. It is advisable in these installs that you add the
following export to your shell config files.

```
export ALGORAND\_DATA=/var/lib/algorand
```

When the Sync Time is zero consistently, it will be close to, if not
all the way, synced.

![A screenshot of a cell phone Description automatically generated](images/media/image2.png)

**Alternatives**

The above process will take several hours to sync, so there are two
alternatives that can be used in the meantime. Each option provides an
Algod token and a Server URL. These values will be needed in your
solution code as well as the sample hackathon lab exercises.

**Option 1 -Algorand Hackathon**

The API Token and Server address can be used in the Hackathon. Once the
hackathon is over, you will need to use your own node or one from
Purestake (see Option 2).

**API Token**
```
ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1
```

**Server Address**

[http://hackathon.algodev.network:9100/](http://hackathon.algodev.network:9100/)

The above service is set to Full Archival Indexer Nodes.

**Option 2 - Purestake Token**

PureStake is offering up a token for this hackathon. You do not need to
register. This is free for use during the hackathon.

PureStake's token declaration works with the Algorand JavaScript SDK
with a minor update to client instantiation in return for many benefits.

Additional SDK support is in progress.

**Examples of a PureStake GET and POST**

* [GET](https://github.com/PureStake/api-examples/blob/master/javascript-examples/get_versions.js)
[versions](https://github.com/PureStake/api-examples/blob/master/javascript-examples/get_versions.js)
* [POST](https://github.com/PureStake/api-examples/blob/master/javascript-examples/submit_tx.js)
[transaction](https://github.com/PureStake/api-examples/blob/master/javascript-examples/submit_tx.js)
* [GET Transaction By](https://github.com/PureStake/api-examples/blob/master/javascript-examples/get_tx.js)
[Id](https://github.com/PureStake/api-examples/blob/master/javascript-examples/get_tx.js)

After the Hackathon is over, you can register to receive a new token
here:

[https://www.purestake.com/algorand-api](https://www.purestake.com/algorand-api)

More details here:

[https://www.purestake.com/technology/algorand-services/](https://www.purestake.com/technology/algorand-services/)

**API-Key token for both TestNet and MainNet:**

```
B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab
```

TestNet Server URL
address: [https://testnet-algorand.api.purestake.io/ps1](https://testnet-algorand.api.purestake.io/ps1)

MainNet Server URL
address: [https://mainnet-algorand.api.purestake.io/ps1](https://mainnet-algorand.api.purestake.io/ps1)

Purestake offers many benefits including a Basic free tier as well as
Pro and Enterprise levels:

Instant Access to Algorand API in Testnet and Mainnet

* No wait for downloads and blockchain sync times

API is backed by Full Archival Transaction Indexer Nodes

* Full algod API with performant transaction queries

Secure and Reliable Infrastructure

* Automated Highly Available Infrastructure, Managed 24x7x365

Secure communications

* All API traffic over https only

<span id="_Toc17979066" class="anchor"></span>**Verify your data**
**directory**

To verify where your data directory is and that you are running TestNet,
use these two commands:

1. ps aux \| grep algod

![A close up of a mans face Description automatically generated](images/media/image3.png)

2. goal node status -d ~/node/data

![A screenshot of a cell phone Description automatically generated](images/media/image4.png)

## Apply current updates

To manually update use these commands
```
cd ~/node
./update.sh -d ~/node/data
```

To configure Auto-Update see:
[https://developer.algorand.org/docs/configure-auto-update](https:/developer.algorand.org/docs/configure-auto-update)

**Download/Clone Algorand SDKs in your language of choice.**

These are the SDKs available to date. More are on the way.
**Time Estimate - 10 minutes**
