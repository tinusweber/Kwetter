import React from 'react'
import ImageUploader from 'react-images-upload'

export default function ProfilePictureEditor({value, callback}) {
    const changeimage = (  (input) => {
        let reader = new FileReader();
        reader.readAsDataURL(input[0]);
        reader.onloadend = () => {
            callback(reader.result);
                   }
    });

    return (
        <div>
            <ImageUploader
            className="imageUpload"
                withIcon={false}
                buttonText='Choose images'
                onChange={(image) => {
                  changeimage(image)
                }}
                withLabel={false}
                imgExtension={['.jpg', '.gif', '.png', '.gif', '.jpeg']}
                maxFileSize={5242880}
                singleImage={true}
            />
        </div>
    )
}
