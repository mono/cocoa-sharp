#import <Cocoa/Cocoa.h>
#import "CSApplication.h"

int main (int argc, const char * argv[])
{

//		we want to get OOed ASAP so lets call a static method that will get us out of main
	[CSApplication csApplicationMain];
	return 0;
	
}
