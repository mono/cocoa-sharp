#include <Cocoa/Cocoa.h>

SEL SEL_fromString(NSString *str)
{
        return NSSelectorFromString(str);
}
