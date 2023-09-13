import { ChangeEvent, useState, useEffect } from "react"

function ImageUpload() {
    const [selectedImage, setSelectedImage] = useState(null)
    const [imagePreview, setImagePreview] = useState(null)

    const handleImageUpload = (event) => {
        const file = event.target.files?.[0] || null
        setSelectedImage(file)

        if (file) {
            const reader = new FileReader()
            reader.onloadend = () => setImagePreview(reader.result)
            reader.readAsDataURL(file)
        } else {
            setImagePreview(null)
        }
    }

    useEffect(() => {
        localStorage.setItem("complaintImage", `${imagePreview}`)
        if(localStorage.getItem("complaintImage") === "null") {
            localStorage.setItem("imagen", "false")
        } else {
            localStorage.setItem("imagen", "true")
        }
    }, [imagePreview])

    return (
        <>
            <label htmlFor="image-upload" className="cursor-pointer border-black border-[1.8px] pl-4 pr-4 pt-2 pb-2 rounded-md text-black p-2">
                Examinar
            </label>
            <input type="file" id="image-upload" accept="image/*" capture="user" onChange={handleImageUpload} className="hidden" />
        </>
    )
}

export default ImageUpload