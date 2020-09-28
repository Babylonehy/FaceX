FROM ubuntu:18.04
# update soruce list
RUN  sed -i s@/archive.ubuntu.com/@/mirrors.aliyun.com/@g /etc/apt/sources.list
RUN  apt-get clean
# Install trimesh2
RUN apt-get update && apt-get install -y dnsutils apt-utils libxi-dev mesa-common-dev libglu1-mesa-dev build-essential unzip make git wget 
ADD https://gfx.cs.princeton.edu/proj/trimesh2/src/trimesh2-2.16.zip trimesh2-2.16.zip
RUN unzip trimesh2-2.16.zip
WORKDIR /trimesh2
RUN make && cp -v bin.Linux64/* /usr/local/bin/

# Install gflags and glog cmake eigen3 liblapack-dev  libopenblas-dev libsuitesparse-dev
RUN apt-get install -y libgflags-dev libgoogle-glog-dev cmake libeigen3-dev 
RUN apt-get install -y liblapack-dev  libopenblas-dev libsuitesparse-dev libatlas-base-dev
# RUN cp -r /usr/local/include/eigen3/Eigen /usr/local/include 
# Install ceres
WORKDIR / 
ADD http://ceres-solver.org/ceres-solver-1.14.0.tar.gz ceres-solver-1.14.0.tar.gz
RUN tar zxf ceres-solver-1.14.0.tar.gz
RUN mkdir ceres-bin
WORKDIR /ceres-bin
RUN cmake ../ceres-solver-1.14.0
RUN make -j1
RUN make install





