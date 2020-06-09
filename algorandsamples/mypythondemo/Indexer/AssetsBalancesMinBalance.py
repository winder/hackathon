import json
# requires Python SDK version 1.3 or higher
from algosdk.v2client import indexer

data = {
    "indexer_token": "",
    "indexer_address": "http://localhost:8980"
}

# instantiate indexer client
myindexer = indexer.IndexerClient(**data)
# gets assets with a min balance of 200 for AssetID
data = {
    "asset_id": "2044572",
    "min_balance": 200
}
response = myindexer.asset_balances(**data)
print(json.dumps(response, indent=2, sort_keys=True))
