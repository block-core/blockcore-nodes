FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

WORKDIR /usr/local/app/

RUN apt-get update \
    && apt-get install -y curl \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

RUN curl -Ls https://github.com/block-core/blockcore-nodes/releases/download/#{VERSION}#/#{CHAIN}#-#{VERSION}#-linux-x64.tar.gz \
    | tar -xvz -C .

EXPOSE #{PORTS}#

ENTRYPOINT ["dotnet", "#{ASSEMBLY}#.dll"]