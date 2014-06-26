// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <AppKit/AppKit.h>


@interface MainWindowController : NSWindowController {
	NSTextField *_LabelField;
	NSTextFieldCell *_MessageLabel;
    NSImageView *_ImageView;
}

@property (nonatomic, retain) IBOutlet NSTextField *LabelField;

@property (nonatomic, retain) IBOutlet NSTextFieldCell *MessageLabel;
@property (assign) IBOutlet NSImageView *ImageView;

- (IBAction)Click_Button:(id)sender;
- (IBAction)Click_Flip:(id)sender;

@end
