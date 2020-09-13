FROM ubuntu:18.04
# Install trimesh2
RUN apt-get update && apt-get install -y dnsutils apt-utils libxi-dev mesa-common-dev libglu1-mesa-dev build-essential unzip make git wget libgflags-dev
ADD https://gfx.cs.princeton.edu/proj/trimesh2/src/trimesh2-2.16.zip trimesh2-2.16.zip
RUN unzip trimesh2-2.16.zip
WORKDIR /trimesh2
RUN make && cp -v bin.Linux64/* /usr/local/bin/
# Install gflags
RUN apt-get install -y libgflags-dev
# RUN git clone https://github.com/gflags/gflags.git
# Install glog
# RUN git clone https://github.com/google/glog.git
# Install cmake
RUN apt-get install wget
RUN wget https://github.com/Kitware/CMake/releases/download/v3.18.2/cmake-3.18.2-Linux-x86_64.sh \
    -q -O /tmp/cmake-install.sh \
    && chmod u+x /tmp/cmake-install.sh \
    && mkdir /usr/bin/cmake \
    && /tmp/cmake-install.sh --skip-license --prefix=/usr/bin/cmake \
    && rm /tmp/cmake-install.sh

ENV PATH="/usr/bin/cmake/bin:${PATH}"

WORKDIR / 
RUN git clone https://github.com/gflags/gflags.git \
    && cd gflags/ \
    && mkdir build && cd build \
    && ccmake .. \
    && make && make test && make install




