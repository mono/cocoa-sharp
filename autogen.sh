#
#  autogen.sh
#
#  Authors
#    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
#    - Urs C. Muff, Quark Inc., <umuff@quark.com>
#    - Kangaroo, Geoff Norton
#    - Adham Findlay
#
#  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.

#	$Header: /home/miguel/third-conversion/public/cocoa-sharp/autogen.sh,v 1.6 2004/06/16 12:20:26 urs Exp $
#

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
./generator/genstubs.pl

#***************************************************************************
#
# $Log: autogen.sh,v $
# Revision 1.6  2004/06/16 12:20:26  urs
# Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
#
#***************************************************************************
