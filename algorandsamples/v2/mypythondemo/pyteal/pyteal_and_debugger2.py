#!/usr/bin/env python3

#  refund this account every run
#  BI6CRIUTBD6FRFDRPVIMYPZHLZ2H5K3F5H2MSBHONCN4RYM5I72MQOHEOU
from pyteal import *

import base64
import os
from algosdk import account, algod, encoding, mnemonic, transaction
from algosdk.v2client import algod
from algosdk.future import transaction
from algosdk.future.transaction import PaymentTxn, LogicSig, StateSchema, ApplicationCreateTxn
from algosdk.v2client.models.dryrun_source import DryrunSource
from algosdk.v2client.models.dryrun_request import DryrunRequest

import json


def write_drr(res, contents):
    dir_path = os.path.dirname(os.path.realpath(__file__))
    path = os.path.join(dir_path, res)
    data = encoding.msgpack_encode(contents)
    data = base64.b64decode(data)
    with open(path, "wb") as fout:fout.write(data)
    return

def write_teal(res, contents):
    dir_path = os.path.dirname(os.path.realpath(__file__))
    path = os.path.join(dir_path, res)   
    f = open(path, "w")
    f.write(str(contents))
    f.close()
    return

def wait_for_confirmation(client, txid):
    """
    Utility function to wait until the transaction is
    confirmed before proceeding.
    """
    last_round = client.status().get('last-round')
    txinfo = client.pending_transaction_info(txid)
    while not (txinfo.get('confirmed-round') and txinfo.get('confirmed-round') > 0):
        print("Waiting for confirmation")
        last_round += 1
        client.status_after_block(last_round)
        txinfo = client.pending_transaction_info(txid)
    print("Transaction {} confirmed in round {}.".format(
        txid, txinfo.get('confirmed-round')))
    return txinfo


# return dry run request (needed for debugging)
def dryrun_drr(lstx, mysource):
    sources = []
    if (mysource != None):
        # source
        sources = [DryrunSource(
            field_name="lsig", source=mysource, txn_index=0)]
    drr = DryrunRequest(txns=[lstx], sources=sources)
    return drr

# pyteal template Hash Time Lock Contracts (htlc)
# resoures
#  
#  https://developer.algorand.org/tutorials/hash-time-lock-contract-pyteal
#  https://developer.algorand.org/docs/reference/teal/templates/htlc/
#  Hash Time Lock Contracts are contract accounts that can disburse funds when the correct hash
#     preimage (“password”) is passed as an argument.
#     If the funds are not claimed with the password after a
#     certain period of time, the original owner can reclaim them.
#  using debugger resources
#  https://developer.algorand.org/docs/features/asc1/debugging/#using-the-teal-debugger

# """ HTLC
# """
# hash_function = "Sha256"
# hash_img = Bytes("base64", "QzYhq9JlYbn2QdOMrhyxVlNtNjeyvyJc/I8d8VAGfGc=")
# timeout = Int(5555)
# max_fee = Int(2000)
# tmpl_rcv = Addr("ZZAF5ARA4MEC5PVDOP64JM5O5MQST63Q2KOY2FLYFLXXD3PFSNJJBYAFZM")
# owner = Addr("GD64YIY3TWGDMCNPP553DZPPR6LDUSFQOIJVFDPPXWEG3FVOJCCDBBHU5A")

# def htlc(tmpl_hash_img=hash_img,
#          tmpl_timeout=timeout,
#          tmpl_owner=owner,
#          tmpl_max_fee=max_fee,
#          tmpl_hash_fn=Sha256,
#          tmpl_rcv=tmpl_rcv):

#     fee_cond = Txn.fee() <= tmpl_max_fee
#     type_cond = Txn.type_enum() == Int(1)
#     amount_cond = Txn.amount() == Int(0)
#     r_cond = Txn.receiver() == Global.zero_address()
#     recv_cond = (Txn.close_remainder_to() == tmpl_rcv).And(
#         tmpl_hash_fn(Arg(0)) == tmpl_hash_img)
#     esc_cond = (Txn.close_remainder_to() == tmpl_owner).And(
#         Txn.first_valid() > tmpl_timeout)

#     htlc_core = fee_cond.And(type_cond).And(r_cond).And(
#         amount_cond).And(recv_cond.Or(esc_cond))
#     return htlc_core


# tmpl_hash_fn = Sha256
# tmpl_hash_img = Bytes("base64", "QzYhq9JlYbn2QdOMrhyxVlNtNjeyvyJc/I8d8VAGfGc=")
# tmpl_timeout = Int(5000000)
# tmpl_max_fee = Int(2000)
# tmpl_rcv = Addr("YGO2HOKUZL5OU23Y6MIVWG6WGAGIUMDSQIXJRRHMOF2ERN7DSJRCINBKS4")
# tmpl_owner = Addr("3TNNHYBR7V4NM4DGE36JY6POVYLLJO62EIAKDKKLT33PYULJUBHVJBNUFI")

# teal_source = htlc(tmpl_hash_img, tmpl_timeout, tmpl_owner,
#                    tmpl_max_fee, tmpl_hash_fn, tmpl_rcv).teal()
creator_mnemonic = "price clap dilemma swim genius fame lucky crack torch hunt maid palace ladder unlock symptom rubber scale load acoustic drop oval cabbage review abstract embark"
user_mnemonic = "unlock garage rack news treat bonus census describe stuff habit harvest imitate cheap lemon cost favorite seven tomato viable same exercise letter dune able add"


#algod_address = "http://localhost:4002"
#algod_token = "4063ef7e83ce9237eb669ff94408216c14bc83c09f6743006174c7b633ade2b9"
#creator_mnemonic = "hover appear scorpion cricket almost congress senior timber also maple depth sorry gallery chef wool enlist harsh sad rate luggage general pioneer glove about diagram"
# TODO: //END

# declare application state storage (immutable)
local_ints = 1
local_bytes = 1
global_ints = 1
global_bytes = 0
global_schema = StateSchema(global_ints, global_bytes)
local_schema = StateSchema(local_ints, local_bytes)


def get_private_key_from_mnemonic(mn):
    private_key = mnemonic.to_private_key(mn)
    return private_key

def approval_program_initial():
    # on_creation = None
    # on_deletion = None
    # on_update = None
    # on_closeout = None
    # on_optin = Int(1)
    # on_clear = None
    get_counter = App.globalGet(Bytes("counter"))
    counter_logic = Seq([
        # read global state, increment the value, and put the updated value back into global state
        App.globalPut(Bytes("counter"), get_counter + Int(1)),

        # read from global state and return
        Return(get_counter)
    ])

    # handlers = Cond(
    #     [Txn.application_id() == Int(0), on_creation],
    #     [Txn.on_completion() == OnComplete.DeleteApplication, on_deletion],
    #     [Txn.on_completion() == OnComplete.UpdateApplication, on_update],
    #     [Txn.on_completion() == OnComplete.CloseOut, on_closeout],
    #     [Txn.on_completion() == OnComplete.OptIn, on_optin],
    #     [Txn.on_completion() == OnComplete.ClearState, on_clear]
    # )

    return counter_logic


def approval_program_refactored():
    # on_creation = None
    # on_deletion = None
    # on_update = None
    # on_closeout = None
    # on_optin = Int(1)
    # on_clear = None
    get_localcounter = App.localGet(Int(0), Bytes("localcounter"))
    user_account = mnemonic.to_public_key(user_mnemonic)
    get_counter = App.globalGet(Bytes("counter"))
    counter_logic = Seq([
        # read global state, increment the value, and put the updated value back into global state
        App.globalPut(Bytes("counter"), get_counter + Int(1)),
        App.localPut(Int(0), Bytes("localcounter"),
                     get_localcounter + Int(1)),
        # read from global state and return
        Return(get_localcounter)
    ])

    return counter_logic

def clear_state_program():
    return Int(1)
# create new application


def compile_program(client, source_code):
    compile_response = client.compile(source_code.decode('utf-8'))
    return base64.b64decode(compile_response['result'])

def create_app(client, private_key, approval_program, clear_program, global_schema, local_schema):
    # define sender as creator
    sender = account.address_from_private_key(private_key)

    # declare on_complete as NoOp
    on_complete = transaction.OnComplete.NoOpOC.real

# get node suggested parameters
    params = client.suggested_params()
    # comment out the next two (2) lines to use suggested fees
    params.flat_fee = True
    params.fee = 1000

    # create unsigned transaction
    txn = ApplicationCreateTxn(sender, params, on_complete,
                                           approval_program, clear_program,
                                           global_schema, local_schema)

    # sign transaction
    signed_txn = txn.sign(private_key)
    tx_id = signed_txn.transaction.get_txid()

    # send transaction
    client.send_transactions([signed_txn])

    # await confirmation
    wait_for_confirmation(client, tx_id)

    # display results
    transaction_response = client.pending_transaction_info(tx_id)
    app_id = transaction_response['application-index']
    print("Created new app-id: ", app_id)

    return app_id


# with open('hello_world_approval.teal', 'w') as f:
#     teal_source = compileTeal(approval_program_initial(), Mode.Application)
#     f.write(teal_source)

# with open('hello_world_clear.teal', 'w') as f:
#     clear_source = compileTeal(clear_state_program(), Mode.Application)
#     f.write(compiled)

# with open('hello_world_updated.teal', 'w') as f:
#     teal_updated_source = compileTeal(
#         approval_program_refactored(), Mode.Application)
#     f.write(teal_updated_source)

# print(teal_updated_source)

# program_file_name = "program.teal"


#--------- compile, debug ,  & send transaction using Python SDK ----------

# read TEAL program
# data = load_resource(myprogram)
algod_address = "http://localhost:4001"
algod_token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
# create algod clients
acl = algod.AlgodClient(algod_token, algod_address)

# define private keys
creator_private_key = get_private_key_from_mnemonic(creator_mnemonic)
user_private_key = get_private_key_from_mnemonic(user_mnemonic)

# compile programs
# Note: replace accounts with your creator account number in approval_program_source_initial
# and approval_program_source_refactored
approval_program_source = compileTeal(
    approval_program_initial(), Mode.Application)
write_teal('hello_world.teal', approval_program_source)

teal_updated_source = compileTeal(
    approval_program_refactored(), Mode.Application)
write_teal('hello_world_updated.teal', teal_updated_source)

clear_program_source = compileTeal(clear_state_program(), Mode.Application)
write_teal('hello_world_clear.teal', clear_program_source)

# approval_program = compile_program(
#     acl, approval_program_source)
response = acl.compile(approval_program_source)
programstr = response['result']
t = programstr.encode("ascii")
# program = b"hex-encoded-program"
approval_program = base64.decodebytes(t)

response = acl.compile(clear_program_source)
programstr = response['result']
t = programstr.encode("ascii")
# program = b"hex-encoded-program"
clear_program = base64.decodebytes(t)

# clear_program = compile_program(acl, clear_program_source)

try:

    app_id = create_app(acl, creator_private_key,
                        approval_program, clear_program, global_schema, local_schema)

    # # Compile TEAL program
    # response = acl.compile(approval_program_source)
    # # Print(response)
    # print("Response Result = ", response['result'])
    # print("Response Hash = ", response['hash'])

    # # Create logic sig
    # programstr = response['result']
    # t = programstr.encode("ascii")
    # # program = b"hex-encoded-program"
    # program = base64.decodebytes(t)

    # # Get the program and parameters and use them to create an lsig
    # # For the contract account to be used in a transaction
    # # In this example 'hero wisdom green split loop element vote belt'
    # # hashed with sha256 will produce our image hash
    # # This is the passcode for the HTLC
    # # args = [
    # #     "hero wisdom green split loop element vote belt".encode()
    # # ]

    lsig = LogicSig(approval_program)

    # lsig escrow account must me funded. 
    # BI6CRIUTBD6FRFDRPVIMYPZHLZ2H5K3F5H2MSBHONCN4RYM5I72MQOHEOU

    # get suggested parameters
    params = acl.suggested_params()
    params.flat_fee = True
    params.fee = 1000
    amount = 0   
    print(params.last)
    print(params.first)

    # create a transaction
    closeremainder = "YGO2HOKUZL5OU23Y6MIVWG6WGAGIUMDSQIXJRRHMOF2ERN7DSJRCINBKS4"
    txn = PaymentTxn(
        lsig.address(), params, "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAY5HFKQ",
        amount, closeremainder, None, None, None)

    # Create the LogicSigTransaction with contract account LogicSig
    lstx = transaction.LogicSigTransaction(txn, lsig)

    # get dryrun request
    mydrr = dryrun_drr(lstx, approval_program_source)
    # write drr
    drr_file_name = "mydrr.dr"
    write_drr(drr_file_name, mydrr)
    print("drr file created ... debugger starting - goto chrome://inspect")

    #  START debugging session
    #  either use from terminal in this folder
    # `tealdbg debug program.teal --dryrun-req mydrr.dr`
    #
    # or use this line to invoke debugger
    # and switch to chrome://inspect to inspect and debug
    # (program execution will continue aafter debuigging session completes)

    dir_path = os.path.dirname(os.path.realpath(__file__))
    drrpath = os.path.join(dir_path, drr_file_name)
    programpath = os.path.join(dir_path, 'hello_world.teal')
    stdout, stderr = execute(
        ["tealdbg", "debug", programpath, "--dryrun-req", drrpath])
    txns = [lstx]
    # send raw LogicSigTransaction to network
    txid = acl.send_transaction(lstx)    
    print("Transaction ID: " + txid)
    wait_for_confirmation(acl, txid)
except Exception as e:
    print(e)
