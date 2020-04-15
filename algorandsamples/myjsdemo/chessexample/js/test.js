(function() {

    //Retrieve the token, server and port values for your installation in the algod.net
    //and algod.token files within the data directory
 
    // sandbox
    const token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
    const server = "http://localhost";
    const port = 4001;

    // Algorand Hackathon
    // const token = "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1";
    // const server = "http://hackathon.algodev.network";
    // const port = 9100;  

    // const token = "f1dee49e36a82face92fdb21cd3d340a1b369925cd12f3ee7371378f1665b9b1";
    // const server = "http://127.0.0.1";
    // const port = 8080;

    // betanet
    // const token = "461b6de3bde79b60426d13092eaeb388f417afb431392191b58c632f09f276aa";
    // const server = "http://127.0.0.1";
    // const port = 8080;


    // Purestake
    // one line of code needs to be commented
    // the line at about line ... needs to be uncommmented 
    // (the one that uses posttoken .. it looks like this) - do not uncomment here
    // let algodclient = new algosdk.Algod(posttoken, aserver, aport);

    // const server = "https://testnet-algorand.api.purestake.io/ps1"
    // const port = "";
    // const token = {
    //     'X-API-Key': 'B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab'
    // }
    // In order to submit a transaction the algod client object must be instantiated with
    // the correct content type for sending binary  
    // const posttoken = {
    //     'X-API-Key': 'B3SU4KcVKi94Jap2VXkK83xx38bsv95K5UZm2lab',
    //     'content-type': 'application/x-binary'
    // }


    var chessId = "XZEJNH6XTTOUJLF52XACHI2UIKWJFQ2I4FPN6VL3UJ7MEBVLSTOZXKDVFM";
    var gameId;
    const matchId = "4CAQ5RUW3H35A722NY47HGGVAM5PAEJW4BFLPPZGZTRBLTFGA2WPESCDFM"
    const finalId = "V2SSXDV62XGYTDVWOM6OHCC2CZE6HIFH4W2TRJF2QRPWJAQJCRXJ4YRMRY";
    let acct = null; //algosdk.mnemonicToSecretKey("indicate enlist park situate put until budget olive animal topic win leave shaft cattle coffee pause then swarm bunker glide security educate legend absent history");
    //var acct = algosdk.mnemonicToSecretKey("shift valve labor arrow owner holiday call slice combine damage write maple task chimney hero forest stone pact frog coil autumn depend service ability term");
    var lastGameRequest = null;
    var findMatchID=null;
 
    var timeoutID;

    var lastMove = null;

    var player1 = document.getElementById('from');
    var player2 = document.getElementById('to');

    var myPlayerId = null; //"EYKZU34ZMVNMQC4U5EMRYOCK6MR4BWYTOMYO7B7AJJB3J7Q5EWZZGZ3L7A";
    //var myPlayerId = "7OO5D3RFHO6F5P2KQUJRM6AAISFC5W7SSW7QEUG7JEFW5OMXWTPPJYJVHU";


    player1.value = "";
    player2.value = ""; 
    var currPlayer = 1; //this will be set for the specific player you are 
    var halfMoves = 0;
    var gameStarted = false;

    //var player1 = document.getElementById('from');
    //var player2 = document.getElementById('to');
    //player2.value = "EYKZU34ZMVNMQC4U5EMRYOCK6MR4BWYTOMYO7B7AJJB3J7Q5EWZZGZ3L7A";
    //player1.value = "7OO5D3RFHO6F5P2KQUJRM6AAISFC5W7SSW7QEUG7JEFW5OMXWTPPJYJVHU";
    //var currPlayer = 2;


    var board = null;
    var game = new Chess();

    var $status = $('#status');
    var $playercolor = $('#playercolor');
    var $currentplayer = $('#currentplayer');

    $playercolor.html("No Assigned Team");
    $currentplayer.html("No Account Generated/Recovered");


    var generatePlayer = document.getElementById('generate');
    var recoverPlayer = document.getElementById('recover');
    var requestList = document.getElementById('requestlist');

    var ta = document.getElementById('ta');
    var ga = document.getElementById('account');
    var st = document.getElementById('transaction');
    var bu = document.getElementById('backup');
    var re = document.getElementById('recover');
    var wr = document.getElementById('wrecover');
    var wall = document.getElementById('wallet');
    var fround = document.getElementById('fround');
    var lround = document.getElementById('lround');
    var adetails = document.getElementById('adetails');
    var trans = document.getElementById('trans');
    var transI = document.getElementById('transI');

    var signKey = null;
    var account = null;

    var fm = document.getElementById('findmatch');
    var gId = document.getElementById('chessid');

    var gameRequest = {
        player1: myPlayerId,
        player2: "0",
        stage1Id: "0",
        stage2Id: "0",
        gameId: "0"
    }

    if (generatePlayer) {
        generatePlayer.onclick = function() {
            acct = algosdk.generateAccount();
            account = acct.addr;
            var mnemonic = algosdk.secretKeyToMnemonic(acct.sk);

            prompt("Your address is below. Copy to clipboard: Ctrl+C, Enter", acct.addr);
            prompt("your backup phrase is below. Do not lose! Copy to clipboard: Ctrl+C, Enter", mnemonic);

            $currentplayer.html("You player id: " + acct.addr);
            myPlayerId = acct.addr;

        }
    }
    if (recoverPlayer) {
        recoverPlayer.onclick = function() {
            let ra = prompt("Enter your Backup Phrase");
            acct = algosdk.mnemonicToSecretKey(ra);
            account = acct.addr;

            $currentplayer.html("You player id: " + acct.addr);
            myPlayerId = acct.addr;

        }
    }    
   //function to initiate a match request
    function updateRequest(){
   
        (async () => {
            console.log("player1 " + gameRequest.player1);
            console.log("player2 " + gameRequest.player2);
            console.log("stage 1 " + gameRequest.stage1Id);
            console.log("stage 2 " + gameRequest.stage2Id);
            console.log("gameId " + gameRequest.gameId); 

            let algodclient = new algosdk.Algod(token, server, port);
       //     let algodclient = new algosdk.Algod(posttoken, server, port);

            let params = await algodclient.getTransactionParams();
            let endRound = params.lastRound + parseInt(1000);
            let faddress = myPlayerId;
            txn = {
                "from": faddress,
                "to": matchId,
                "fee": 0,
                "amount": 0,
                "firstRound": params.lastRound,
                "lastRound": endRound,
                "note": algosdk.encodeObj(gameRequest),
                "genesisID": "testnet-v1.0",
                "genesisHash": "SGO1GKSzyE7IEPItTxCByw9x8FmnrCDexi9/cOUJOiI="
            };

            var signedTxn = algosdk.signTransaction(txn, acct.sk);
            console.log(signedTxn.txID);
     
   
            let tx = (await algodclient.sendRawTransaction(signedTxn.blob));
            var textedJson = JSON.stringify(tx, undefined, 4);
            console.log("updated request for match" + textedJson);
            console.log("player1 " + gameRequest.player1);
            console.log("player2 " + gameRequest.player2);
            console.log("stage 1 " + gameRequest.stage1Id);
            console.log("stage 2 " + gameRequest.stage2Id);
            console.log("gameId " + gameRequest.gameId); 
        })().catch(e => {
            console.log(e);
        });


    }

    async function checkPending(){
        console.log("checking pending");
        let pending = 0;
         try{
            let algodclient = new algosdk.Algod(token, server, port);
            let pendingTxs = await algodclient.pendingTransactions(0);
            console.log( "Pending Txs = " + pendingTxs.totalTxns );
            if( pendingTxs.totalTxns > 0){
                for( let j=0; j < pendingTxs.totalTxns; j++){
                    if( pendingTxs.truncatedTxns.transactions[j].from === myPlayerId){
                        let nt = algosdk.decodeObj(pendingTxs.truncatedTxns.transactions[j].note);
                        console.log( "found a pending");
                        var txJson = JSON.stringify(nt, undefined, 4);    
                        console.log( txJson );                         
                        pending = 1;
                        return pending;
                    }
                }
            }
            return pending;

        } catch(e) {
            console.log(e);
        };
        return pending;

    }

    //function to initiate a match request
    async function makeMatchRequest(){
        //if( lastGameRequest !== null){
        //    console.log("already did one request");
        //    return;
        //} 
        console.log("make match request function");

        (async () => {
            gameRequest = {
                player1: myPlayerId,
                player2: "0",
                stage1Id: "0",
                stage2Id: "0",
                gameId: "0"

            }
            console.log("here");
            let pending = await checkPending();
            console.log("Pending "+ pending);
            if( pending ){
                console.log("prevented double match request");
                return;
            }else{
                console.log("No pending txs" + pending);
            }             
            let algodclient = new algosdk.Algod(token, server, port);
         //   let algodclient = new algosdk.Algod(posttoken, server, port);
          
            let params = await algodclient.getTransactionParams();

            let endRound = params.lastRound + parseInt(1000);
            let faddress = myPlayerId;
            txn = {
                "from": faddress,
                "to": matchId,
                "fee": 0,
                "amount": 0,
                "firstRound": params.lastRound,
                "lastRound": endRound,
                "note": algosdk.encodeObj(gameRequest),
                "genesisID": "testnet-v1.0",
                "genesisHash": "SGO1GKSzyE7IEPItTxCByw9x8FmnrCDexi9/cOUJOiI="
            };

            var signedTxn = algosdk.signTransaction(txn, acct.sk);
            lastGameRequest = signedTxn.txID;
            console.log(signedTxn.txID);

            let tx = (await algodclient.sendRawTransaction(signedTxn.blob));
            var textedJson = JSON.stringify(tx, undefined, 4);
            console.log("GGGGG" + textedJson);

        })().catch(e => {
            console.log(e);
        });
    }
    function updateBasedOnTx( tx ){
        var nt = algosdk.decodeObj(tx.note);
        console.log("player1 " + nt.player1);
        console.log("player2 " + nt.player2);
        console.log("stage 1 " + nt.stage1Id);
        console.log("stage 2 " + nt.stage2Id);
        console.log("gameId " + nt.gameId);  
        gameId = nt.gameId;
        //if( gameStarted) return;
        if( nt.player1 === myPlayerId){
            if( nt.player2 === "0"){
                //still waiting on a game
                return;
            }
            //this means I have player2 and a stage1d

            if(nt.stage2Id === "0" && nt.stage1Id === "1" ){
                gameRequest.stage2Id = "1";
                gameRequest.gameId = algosdk.generateAccount().addr;
                gameId  = gameRequest.gameId;
                gameRequest.player1 = nt.player1;
                gameRequest.player2 = nt.player2;
                gameRequest.stage1Id = nt.stage1Id;

                gId.value = gameId;
                player1.value = nt.player1;
                player2.value = nt.player2;
                currPlayer = 1;
                console.log("starting game as Player 1");     
                startGame();
                gameStarted = true;
               //window.clearTimeout(findMatchID);
                updateRequest();                
                return;
            }
        }      
        if( nt.player2 === myPlayerId){
            //this assumes there is a player1
            if( nt.stage2Id === "1"){
                gameId  = nt.gameId;
                gId.value = gameId;
                player1.value = nt.player1;
                player2.value = nt.player2;
                gameId = nt.gameId;
                currPlayer = 2;
                console.log("Starting Game as Player 2" + gameId);     
                startGame();
                gameStarted = true;
                //window.clearTimeout(findMatchID); 
            }
            return;
        }
        if( nt.player1 !== myPlayerId && nt.player2 === "0" ){
            //i have not found a player but someone else sent out a request
            if(gameRequest.player2 === "0"){
                //i now become player 2
                gameRequest.player2 = myPlayerId;
                gameRequest.player1 = nt.player1;                
                gameRequest.stage1Id = "1";
                updateRequest();
                return;
            }

        }
    }
    function keepLookingForGame(){
            //need to verify we have acct.sk here

            let algodclient = new algosdk.Algod(token, server, port);
            //this only finds match requests within the last hour or so
            if (player1.value !== null) {
                (async () => {
                    let pending = await checkPending();
                    if( pending ){
                        console.log("prevented looking up while pending request");
                        let paramsPending = await algodclient.getTransactionParams();
                        await algodclient.statusAfterBlock(paramsPending.lastRound+1);
                        return;
                    }

                    let params = await algodclient.getTransactionParams();
                    let txts = (await algodclient.transactionByAddress(matchId, params.lastRound - 10, params.lastRound));                    
                    if (txts.transactions) {
                        var textedJson = JSON.stringify(txts.transactions, undefined, 4);
                        //console.log("txs in keepLookingForGame" + textedJson);
                        //console.log( "Number of Txts " + txts.transactions.length );
                        updateBasedOnTx( txts.transactions[txts.transactions.length-1] );
                        //updateBasedOnTx( txts.transactions[0] );
                        if( !gameStarted ){
                            console.log( "trigger 1");
                            await algodclient.statusAfterBlock(params.lastRound+1);
                            keepLookingForGame();
                            //findMatchID = window.setTimeout(keepLookingForGame, 8000);
                        }
                        return;

                    }else{
                        //no match request so initiate one
                        console.log( "making match request")
                        console.log(  new Date().toLocaleTimeString());
                        let a = makeMatchRequest();
                        let b = algodclient.statusAfterBlock(params.lastRound+2);
                        await Promise.all([a,b]);
                        console.log(  new Date().toLocaleTimeString());
                        console.log( "trigger 2");
                        keepLookingForGame();
                        //findMatchID = window.setTimeout(keepLookingForGame, 2000);
                        return;
                    }

                })().catch(e => {
                    console.log(e);
                });

            }

    }
    async function showCurrentGameRequest(){
        (async () => {
                console.log("checking game requests");
                let algodclient = new algosdk.Algod(token, server, port);
                let params = await algodclient.getTransactionParams();
                let txts = (await algodclient.transactionByAddress(matchId, params.lastRound - 10, params.lastRound));                    
                if (txts.transactions) {
                        var textedJson = JSON.stringify(txts.transactions, undefined, 4);
                    for (let i = 0; i < txts.transactions.length; i++) {
                        var len = txts.transactions.length - 1;
                        var nt = algosdk.decodeObj(txts.transactions[i].note);
                        if( nt.gameId === "0" && nt.stage1Id === "0" && nt.stage2Id === "0" ){
                            if( nt.player1 !== "0"){
                                document.getElementById("requestlist").innerHTML = "";
                                var li = document.createElement("li");
                                li.setAttribute('id',nt.player1);
                                li.appendChild(document.createTextNode(nt.player1));
                                requestList.appendChild(li);
                            }

                        }

                    }   
                }else{
                    document.getElementById("requestlist").innerHTML = "";
                    var li = document.createElement("li");
                    li.setAttribute('id',"none");
                    li.appendChild(document.createTextNode("No Current Game Requests"));
                    requestList.appendChild(li);                    
                }
                await algodclient.statusAfterBlock(params.lastRound+1);
                showCurrentGameRequest();

        })().catch(e => {
            console.log(e);
        });
    }
    //try to find an existing match
    if (fm) {
        fm.onclick = function() {
            if( myPlayerId == null){
                alert("You must generate or recover a player first");
                return;
            }
            gId.value = "";
            player2.value = "";
            gameStarted = false;
            document.body.style.cursor = "wait";
            $playercolor.html("Searching For A Match");
            keepLookingForGame();
        }
    }

    function onDragStart(source, piece, position, orientation) {
        // do not pick up pieces if the game is over
        if (game.game_over()) return false

        // only pick up pieces for the side to move
        if ((game.turn() === 'w' && piece.search(/^b/) !== -1) ||
            (game.turn() === 'b' && piece.search(/^w/) !== -1)) {
            return false;
        }
        console.log(currPlayer);
        console.log(game.turn());

        if ((currPlayer === 1 && game.turn() === 'b') ||
            (currPlayer === 2 && game.turn() === 'w')) {
            return false;
        }
    }

    function onDrop(source, target) {
        // see if the move is legal
        var move = game.move({
            from: source,
            to: target,
            promotion: 'q' // NOTE: always promote to a queen for example simplicity
        })

        // illegal move
        if (move === null) return 'snapback'


        updateStatus()
    }

    function checkTX() {

        let algodclient = new algosdk.Algod(token, server, port);
        (async () => {
            let pending = await checkPending();
            if( pending ){
                console.log("do not send request until last move is resolved");
                let params = await algodclient.getTransactionParams();
                await algodclient.statusAfterBlock(params.lastRound+1);
                checkTX();
                return;
            }            
            //alert( txid.value );
            let tx = (await algodclient.transactionById(lastMove));
            var textedJson = JSON.stringify(tx, undefined, 4);
            //console.log(textedJson);
            //console.log("Calling Loop");
            //startGame();
            return;
        })().catch(e => {
            console.log("Error" + e);
        });
    }

    function sendMove() {
        //var acct = algosdk.generateAccount();
        var move = {
            player1: player1.value,
            player2: player2.value,
            fen: game.fen(),
            gameId: gameId,
            halfMoves: halfMoves
        };
        let faddress = myPlayerId;

       let algodclient = new algosdk.Algod(token, server, port);
    //    let algodclient = new algosdk.Algod(posttoken, server, port);

        (async () => {
            let params = await algodclient.getTransactionParams();
            let endRound = params.lastRound + parseInt(1000);
            txn = {
                "from": faddress,
                "to": chessId,
                "fee": 0,
                "amount": 0,
                "firstRound": params.lastRound,
                "lastRound": endRound,
                "note": algosdk.encodeObj(move),
                "genesisID": "testnet-v1.0",
                "genesisHash": "SGO1GKSzyE7IEPItTxCByw9x8FmnrCDexi9/cOUJOiI="
            };

            var signedTxn = algosdk.signTransaction(txn, acct.sk);
            //console.log(signedTxn.txID);

            let tx = (await algodclient.sendRawTransaction(signedTxn.blob));
            var textedJson = JSON.stringify(tx, undefined, 4);
            console.log(textedJson);
            //console.log(tx.txId);
            lastMove = tx.txId;
            await algodclient.statusAfterBlock(params.lastRound+1);
            checkTX();
            return;            
            //console.log( "GAME MOVES " + game.moves() );
        })().catch(e => {
            console.log(e);
        });
    }
    // update the board position after the piece snap
    // for castling, en passant, pawn promotion
    function onSnapEnd() {

        //console.log( game.fen() );  
        board.position(game.fen())
        halfMoves++;
        sendMove()
    }

    function updateStatus() {
        var status = ''

        var moveColor = 'White'
        if (game.turn() === 'b') {
            moveColor = 'Black'
        }

        // checkmate?
        if (game.in_checkmate()) {
            status = 'Game over, ' + moveColor + ' is in checkmate.'
            //need to update winner address
        }

        // draw?
        else if (game.in_draw()) {
            status = 'Game over, drawn position'
            //need to update record
        }

        // game still on
        else {
            status = moveColor + ' to move'

            // check?
            if (game.in_check()) {
                status += ', ' + moveColor + ' is in check'
            }
        }

        $status.html(status)

    }

    var config = {
        draggable: true,
        position: 'start',
        onDragStart: onDragStart,
        onDrop: onDrop,
        onSnapEnd: onSnapEnd
    }
    board = Chessboard('myBoard', config)

    function getGameTurn(fen) {
        var tokens = fen.split(/\s+/);
        return parseInt(tokens[5]);
    }

    function checkOtherPlayerMoved() {
        console.log("checking other player");

        let address = player1.value;
        if (game.turn() === 'b') address = player2.value;

        let algodclient = new algosdk.Algod(token, server, port);
        //submit the transaction
        (async () => {
            let params = (await algodclient.getTransactionParams());
            let txts = (await algodclient.transactionByAddress(chessId, params.lastRound - 20, params.lastRound));
            //let txts = (await algodclient.transactionByAddress(chessId)); //, params.lastRound - 100000, params.lastRound));

            //console.log(txts.transactions[0]);
              if (txts.transactions) {
                for (let i = 0; i < txts.transactions.length; i++) {
                    var len = txts.transactions.length - 1;
                    //console.log(txts.transactions[i]);
                    //if( ((txts.transactions[i].from === player2.value) && (currPlayer === 1)) || ((txts.transactions[i].from === player1.value) && (currPlayer === 2))){
                        var nt = algosdk.decodeObj(txts.transactions[i].note);
                        var tJ = JSON.stringify(nt, undefined, 4);
                        //console.log( "$$$$$$$$$$$$$$$ " + tJ );
                        if( nt.gameId === gameId ){
                            //console.log(nt.player1);
                            //console.log(nt.fen);
                            var blockGameTurn = getGameTurn(nt.fen);
                            var textedJson = JSON.stringify(nt, undefined, 4);
                            console.log("#############" + textedJson)
                            if (halfMoves < nt.halfMoves) {
                                halfMoves = nt.halfMoves;
                                game.load(nt.fen);
                                board.position(game.fen());
                                updateStatus();
                                console.log("Updated Status " + halfMoves + " nt half " + nt.halfMoves);
                                break;
                            }
                            
                        }
                    //}
                }
            }

        })().catch(e => {
            console.log(e);
        });


    }

    function startGame() {
        let algodclient = new algosdk.Algod(token, server, port);
        document.body.style.cursor = "default";
        if( currPlayer === 1){
            $playercolor.html("You are: white player");
        }else{
            $playercolor.html("You are: black player");
        }

        if( game.in_draw() || game.in_checkmate()){
            console.log("Game Over Find a new Match");
            return;
        }
        (async () => {
            checkOtherPlayerMoved();
            let params = (await algodclient.getTransactionParams());
            await algodclient.statusAfterBlock(params.lastRound+1);
            startGame()
            return;
        })().catch(e => {
            console.log("Error" + e);
        });

    }
    updateStatus();
    showCurrentGameRequest();
 

})();