import json
import base64
# requires Python SDK version 1.3 or higher
from algosdk.v2client import indexer
encodednote = base64.b64encode('showing prefix'.encode())
data = {
    "indexer_token": "",
    "indexer_address": "http://localhost:8980"
}
# instantiate indexer client
myindexer = indexer.IndexerClient(**data)
data = {
    "note_prefix": base64.b64decode(encodednote)
}
response = myindexer.search_transactions(**data)
print("note_prefix = " +
      json.dumps(response, indent=2, sort_keys=True))
