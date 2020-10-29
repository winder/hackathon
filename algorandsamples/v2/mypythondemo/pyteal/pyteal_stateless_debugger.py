#!/usr/bin/env python3

#  refund this account every run
#  BI6CRIUTBD6FRFDRPVIMYPZHLZ2H5K3F5H2MSBHONCN4RYM5I72MQOHEOU
from pyteal import *

import base64
import os
from algosdk import account, algod, encoding, mnemonic, transaction
from algosdk.v2client import algod
from algosdk.future.transaction import PaymentTxn, LogicSig
from algosdk.v2client.models.dryrun_source import DryrunSource
from algosdk.v2client.models.dryrun_request import DryrunRequest

import json


def write_drr(res, contents):
    dir_path = os.path.dirname(os.path.realpath(__file__))
    path = os.path.join(dir_path, res)
    data = encoding.msgpack_encode(contents)
    data = base64.b64decode(data)
    with open(path, "wb") as fout:
        fout.write(data)
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


""" HTLC
"""
hash_function = "Sha256"
hash_img = Bytes("base64", "QzYhq9JlYbn2QdOMrhyxVlNtNjeyvyJc/I8d8VAGfGc=")
timeout = Int(5555)
max_fee = Int(2000)
tmpl_rcv = Addr("ZZAF5ARA4MEC5PVDOP64JM5O5MQST63Q2KOY2FLYFLXXD3PFSNJJBYAFZM")
owner = Addr("GD64YIY3TWGDMCNPP553DZPPR6LDUSFQOIJVFDPPXWEG3FVOJCCDBBHU5A")


def htlc(tmpl_hash_img=hash_img,
         tmpl_timeout=timeout,
         tmpl_owner=owner,
         tmpl_max_fee=max_fee,
         tmpl_hash_fn=Sha256,
         tmpl_rcv=tmpl_rcv):
    fee_cond = Txn.fee() <= tmpl_max_fee
    type_cond = Txn.type_enum() == Int(1)
    amount_cond = Txn.amount() == Int(0)
    r_cond = Txn.receiver() == Global.zero_address()
    recv_cond = (Txn.close_remainder_to() == tmpl_rcv).And(
        tmpl_hash_fn(Arg(0)) == tmpl_hash_img)
    esc_cond = (Txn.close_remainder_to() == tmpl_owner).And(
        Txn.first_valid() > tmpl_timeout)
    htlc_core = fee_cond.And(type_cond).And(r_cond).And(
        amount_cond).And(recv_cond.Or(esc_cond))
    return htlc_core

tmpl_hash_fn = Sha256
tmpl_hash_img = Bytes("base64", "QzYhq9JlYbn2QdOMrhyxVlNtNjeyvyJc/I8d8VAGfGc=")
tmpl_timeout = Int(5000000)
tmpl_max_fee = Int(2000)
tmpl_rcv = Addr("YGO2HOKUZL5OU23Y6MIVWG6WGAGIUMDSQIXJRRHMOF2ERN7DSJRCINBKS4")
tmpl_owner = Addr("3TNNHYBR7V4NM4DGE36JY6POVYLLJO62EIAKDKKLT33PYULJUBHVJBNUFI")

teal_source = compileTeal(htlc(tmpl_hash_img, tmpl_timeout, tmpl_owner,
                               tmpl_max_fee, tmpl_hash_fn, tmpl_rcv), Mode.Signature)
print(teal_source)

program_file_name = "program.teal"

write_teal(program_file_name, teal_source)
#--------- compile, debug ,  & send transaction using Python SDK ----------

# read TEAL program
# data = load_resource(myprogram)
algod_address = "http://localhost:4001"
algod_token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
# create algod clients
acl = algod.AlgodClient(algod_token, algod_address)
try:

    # Compile TEAL program
    response = acl.compile(teal_source)
    # Print(response)
    print("Response Result = ", response['result'])
    print("Response Hash = ", response['hash'])

    # Create logic sig
    programstr = response['result']
    t = programstr.encode("ascii")
    # program = b"hex-encoded-program"
    program = base64.decodebytes(t)

    # Get the program and parameters and use them to create an lsig
    # For the contract account to be used in a transaction
    # In this example 'hero wisdom green split loop element vote belt'
    # hashed with sha256 will produce our image hash
    # This is the passcode for the HTLC
    args = [
        "hero wisdom green split loop element vote belt".encode()
    ]

    lsig = LogicSig(program, args)

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
    mydrr = dryrun_drr(lstx, teal_source)
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
    programpath = os.path.join(dir_path, program_file_name)
    stdout, stderr = execute(
        ["tealdbg", "debug", programpath, "--dryrun-req", drrpath])
    txns = [lstx]
    # send raw LogicSigTransaction to network
    txid = acl.send_transaction(lstx)
    print("Transaction ID: " + txid)
    wait_for_confirmation(acl, txid)
except Exception as e:
    print(e)
