BUILD=../../build
APP=CurrencyConverter.app

all: test

test: CurrencyConverter.cs
	rm -Rf $(APP)
	mkdir -p $(APP)/Contents/Resources
	mcs CurrencyConverter.cs -out:$(APP)/Contents/Resources/Test.exe -r:$(BUILD)/Apple.Foundation.dll -r:$(BUILD)/Apple.AppKit.dll
	mkdir -p $(APP)/Contents/MacOS
	cp ../../sample/SimpleWindow/*.icns $(APP)/Contents/Resources
	cp ./Info.plist $(APP)/Contents/Info.plist
	cp $(BUILD)/*.dll* $(APP)/Contents/Resources
	cp $(BUILD)/*Glue* $(APP)/Contents/Resources
	cp $(BUILD)/CocoaSharpLoader $(APP)/Contents/MacOS/CurrencyConverter
	cp -r CurrencyConverter.nib $(APP)/Contents/Resources

run: test
	./$(APP)/Contents/MacOS/CurrencyConverter
