# Please, oh please, add to this - C.J.
#  $Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/typeConversion.pl,v 1.2 2004/06/17 13:06:27 urs Exp $

%typeConversion = 
    # ObjC        => C#
    ( "int"       => "int",
      "int *"     => "IntPtr",
      "NSString " => "Apple.Foundation.NSString",
      "id"        => "IntPtr",
    )

#  $Log: typeConversion.pl,v $
#  Revision 1.2  2004/06/17 13:06:27  urs
#  - release cleanup: only call release when requested
#  - loader cleanup
#
#  Revision 1.1  2004/06/17 04:32:43  cjcollier
#  A map between objC and C# types
#
