BUILD=../../build
APP=test.app

all: test

test: nib-test.cs
	rm -Rf $(APP)
	mkdir -p $(APP)/Contents/Resources
	mcs nib-test.cs -out:$(APP)/Contents/Resources/Test.exe -r:$(BUILD)/Apple.Foundation.dll -r:$(BUILD)/Apple.AppKit.dll
	mkdir -p $(APP)/Contents/MacOS
	cp ../../sample/SimpleWindow/*.icns $(APP)/Contents/Resources
	cp ../../monodoc/monodoc.plist $(APP)/Contents/Info.plist
	cp $(BUILD)/*.dll* $(APP)/Contents/Resources
	cp $(BUILD)/*Glue* $(APP)/Contents/Resources
	cp $(BUILD)/CocoaSharpLoader $(APP)/Contents/MacOS/monodoc
	cp -r step3-MainMenu.nib $(APP)/Contents/Resources

run: test
	./$(APP)/Contents/MacOS/monodoc
