LIB=/Library/Frameworks/CocoaSharp.framework/Libraries
BIN=/Library/Frameworks/CocoaSharp.framework/Commands
APP=CurrencyConverter.app

all: test

test: CurrencyConverter.cs
	rm -Rf $(APP)
	mkdir -p $(APP)/Contents/Resources
	mcs CurrencyConverter.cs -out:$(APP)/Contents/Resources/Test.exe -r:$(LIB)/Apple.Foundation.dll -r:$(LIB)/Apple.AppKit.dll
	mkdir -p $(APP)/Contents/MacOS
	cp ./mono.icns $(APP)/Contents/Resources
	cp ./Info.plist $(APP)/Contents/Info.plist
	cp $(LIB)/*.dll* $(APP)/Contents/Resources
	cp $(LIB)/*Glue* $(APP)/Contents/Resources
	cp $(BIN)/CocoaSharpLoader $(APP)/Contents/MacOS/CurrencyConverter
	cp -r CurrencyConverter.nib $(APP)/Contents/Resources

run: test
	./$(APP)/Contents/MacOS/CurrencyConverter
