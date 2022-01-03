FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /usr/local/app/

RUN apt-get update \
    && apt-get install -y curl libsnappy-dev libc-dev libc6-dev libc6 unzip \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

RUN curl -Ls https://github.com/block-core/blockcore-nodes/releases/download/#{VERSION}#/#{CHAIN}#-#{VERSION}#-linux-x64.tar.gz \
    | tar -xvz -C .

EXPOSE #{PORTS}#

ENTRYPOINT ["dotnet", "#{ASSEMBLY}#"]