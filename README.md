# Blockcore Reference Nodes

Reference implementations of Blockcore based blockchains.

*This software is provided without any support, use at your own risk.*

## Available for these blockchains

- BTC - [Bitcoin](https://bitcoin.org/)
- CITY - [City Chain](https://www.city-chain.org)
- CY - [Cybits](https://www.cybits.org)
- EXOS - [ExO Economy](https://economy.openexo.com/)
- IMPLX - [Impleum](https://impleum.com/)
- MOL - [Molie](https://molie.chat/?inv=MTAwMTIx)
- RUTA - [Rutanio](https://www.rutanio.com/)
- STRAT - [Stratis](https://stratisplatform.com/)
- X42 - [x42 Protocol](https://www.x42.tech/)
- XDS - [XDS](https://github.com/sonofsatoshi2020/xds)
- XLR - [Solaris](https://www.solariscoin.com/)
- SBC - [Senior Block Coin](https://seniorblockchain.io/)
- RSC - [Royal Sports City](https://www.royalsportscity.com/)

## Download

Go to the [releases](releases) page to find a unified multi-chain node. Separate downloads for Windows, Linux and macOS is available.

## Docker

All our Blockcore Reference Nodes is published to our [Docker Hub](https://hub.docker.com/orgs/blockcore/repositories).

It is super easy to spin up a new instance of any of the nodes, they all follow the same syntax (name and version).

*We advice on using specific version when using docker, like our example below. The "latest" tag can also be used.*

Run the Blockcore Reference Node for XDS blockchain in interactive mode:

```sh
docker run blockcore/node-multi:latest --chain=XDS
```

To spin up a docker container instance in the background, apply the "-d" tag.

Run the Blockcore Reference Node for City Chain blockchain in background:

```sh
docker run blockcore/node-multi:latest -d --chain=CITY
```

If you want to access or expose the P2P port, RPC port or API port, you must do a port mapping that is specific for the chain you are launching.

You can find the correct and default TCP port for each blockchain on:

[https://chains.blockcore.net/](https://chains.blockcore.net/)

```sh
docker run blockcore/node-multi:latest -p 8080:4335 -d --chain=CITY -apiuri=http://0.0.0.0:4335
```

## Support and compatibility

These are all unofficial reference node software. They are not supported in any way.

The software can be incompatible with the blockchain they are built for.

You run the risk of getting your IP address banned on the blockchain if you run 
node software that violated the blockchain consensus.

Please refer to the official software for individual blockchains for supported node software.
