all: ../../build/Apple.Foundation.dll

../../build/Apple.Foundation.dll: *.cs
	mcs --unsafe -g *.cs -target:library -out:../../build/Apple.Foundation.dll

maintainer-clean: clean

clean:
	touch ../../build/Apple.Foundation.dll
	rm ../../build/Apple.Foundation.dll

test:
	mcs NSAppleScript.cs.gen NSObject.cs INSCopying.cs.gen NSURL.cs.gen INSCoding.cs.gen INSURLHandleClient.cs.gen NSDictionary.cs.gen NSAppleEventDescriptor.cs.gen NSAttributedString.cs.gen NSCoder.cs.gen NSNumber.cs.gen NSURLHandle.cs.gen NSData.cs.gen INSMutableCopying.cs.gen NSValue.cs.gen NSURLHandleStatus.cs.gen
