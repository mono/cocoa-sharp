aclocal
libtoolize --force --copy
automake -a
autoconf
./configure --enable-maintainer-mode $*

# sym link framework
#rm -rf ~/Library/Framework/MonoObjc.framework
#ln -s build/MonoObjc.framework ~/Library/Framework/MonoObjc.framework
