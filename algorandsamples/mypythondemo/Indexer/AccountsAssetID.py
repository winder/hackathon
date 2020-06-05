import json
# requires Python SDK version 1.3 or higher
from algosdk.v2client import indexer

data = {
    "indexer_token": "",
    "indexer_address": "http://localhost:8980"
}

# instantiate indexer client
myindexer = indexer.IndexerClient(**data)
# gets accounts for AssetID, returns 100 max, use next_page to loop for more
data = {
    "asset_id": 312769
}
response = myindexer.accounts(**data)

print(json.dumps(response, indent=2, sort_keys=True))

