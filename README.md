# Blockcore Reference Nodes

Reference implementations of Blockcore based blockchains

This software is provided without any support, use at your own risk.

Please refer to the official software for individual blockchains for supported software.

## Available for these blockchains

- XDS
- CITY - [City Chain](https://www.city-chain.org)


## Manual download

Go to the release page to find the packaged nodes for various chains. Separate downloads for Windows, Linux and macOS is available.

## Docker

All our Blockcore Reference Nodes is published to our [Docker Hub](https://hub.docker.com/orgs/blockcore).

It is super easy to spin up a new instance of any of the nodes, they all follow the same syntax (name and version).

*We advice on using specific version when using docker, like our example below.*

Run the Blockcore Reference Node for XDS blockchain in interactive mode:

```sh
docker run blockcore/node-xds:1.0.4
```

To spin up a docker container instance in the background, apply the "-d" tag.

Run the Blockcore Reference Node for City Chain blockchain in background:

```sh
docker run blockcore/node-city:1.0.4
```
