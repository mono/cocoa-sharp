
#Simple test for OS X
if [ -d /Library/Frameworks/Mono.framework/Versions/Current/share/aclocal ]; then
    aclocal -I /Library/Frameworks/Mono.framework/Versions/Current/share/aclocal
else
    echo "WARNING: Please use the official Mono OS X Packages"
    aclocal
fi


glibtoolize --force --copy
automake -a
autoconf
./configure --enable-maintainer-mode $*

# sym link framework
#rm -rf ~/Library/Framework/MonoObjc.framework
#ln -s build/MonoObjc.framework ~/Library/Framework/MonoObjc.framework
