#include <stdio.h>

void callManagedDelegate(char *methodToInvoke, void *(managedDelegate)(char *m)) {
	printf("NATIVE: callBackToManaged(%s)\n", methodToInvoke);
	managedDelegate(methodToInvoke);
}
