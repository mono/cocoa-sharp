SUBDIRS=

all: CocoaSharpLoader

clean:
	rm -f ../../build/CocoaSharpLoader
	rm -f CocoaSharpLoader
	rm -f *.o

maintainer-clean: clean

CocoaSharpLoader: Loader.o Bundle.o
	gcc Bundle.o Loader.o -o CocoaSharpLoader -framework Cocoa `pkg-config --libs mono`
	cp CocoaSharpLoader ../../build

Loader.o: Loader.c
	gcc -g -c Loader.c -o Loader.o `pkg-config --cflags mono`

Bundle.o: Bundle.m
	gcc -x objective-c -g -c Bundle.m -o Bundle.o
