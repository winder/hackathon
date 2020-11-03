# account_info.py
import json
# requires Python SDK version 1.3 or higher
from algosdk.v2client import indexer

# instantiate indexer client
myindexer = indexer.IndexerClient(indexer_token="", indexer_address="http://localhost:8980")
# token = {
#     'X-API-key': 'WpYvadV1w53mSODr6Xrq77tw0ODcgHAx9iJBn5tb',
# }
# myindexer = indexer.IndexerClient(indexer_token=token,
#                                   indexer_address="https://betanet-algorand.api.purestake.io/idx2")

# response = myindexer.account_info(
#     address="7WENHRCKEAZHD37QMB5T7I2KWU7IZGMCC3EVAO7TQADV7V5APXOKUBILCI")

# response = myindexer.account_info(
#     address="ZV2CISJONFBUIUIYFKQJ2UISXPDESCJWH6WO6DKRMXUJB7HDBSABOTYFKU")
# response = myindexer.account_info(
#     address="NI2EDLP2KZYH6XYLCEZSI5SSO2TFBYY3ZQ5YQENYAGJFGXN4AFHPTR3LXU")

response = myindexer.account_info(
    address="7DCJZKC4JDUKM25W7TDJ5XRTWGUTH6DOG5WARVA47DOCXQOTB4GMLNVW7I")

# ZV2CISJONFBUIUIYFKQJ2UISXPDESCJWH6WO6DKRMXUJB7HDBSABOTYFKU

# https://betanet-algorand.api.purestake.io/idx2
# response = myindexer.account_info(
#     address="6SKIRCMLFSSY3EJUC6QGFM3TFIJH72ZYUHX7GCUBDBUBYCAHJBJ5PWB344")

print("Account Info: " + json.dumps(response, indent=2, sort_keys=True))
