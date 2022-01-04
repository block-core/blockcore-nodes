# Cirrus Node

A customized Cirrus full node to support the blockcore indexer and explorer.

## Getting Started

The full node is run in exactly the same way as the standard Strax full node.

* **DEVNET** - `dotnet run -devmode=miner`
* **TESTNET** - `dotnet run -testnet`
* **MAINNET** - `dotnet run`

## Configuration

The node is configured to log to application insights, providing an instrumentation key is provided. The instrumentation key can be provided in the `configuration.json` settings, user-secrets or environment variables.
