
#Simple test for OS X
if [ -d /Library/Frameworks/Mono.framework/Versions/0.95/share/aclocal ]; then
    aclocal -I /Library/Frameworks/Mono.framework/Versions/0.95/share/aclocal
else
    echo "Not OS X"
    aclocal
fi


libtoolize --force --copy
automake -a
autoconf
./configure --enable-maintainer-mode $*

# sym link framework
#rm -rf ~/Library/Framework/MonoObjc.framework
#ln -s build/MonoObjc.framework ~/Library/Framework/MonoObjc.framework
