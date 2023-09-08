import { useState } from "react"

const Notifications = () => {
    const [isOpen, setIsOpen] = useState(false)

    return (
        <div className={`absolute top-16 -ml-40 shadow-2xl bg-white h-fit rounded-lg`}>
            <ul className="relative p-4 z-10">
                <p className="text-black font-medium w-[200px] text-center">Notificaciones</p>
                <li className="text-sm mt-5 text-black"> <span className="font-semibold mr-2 text-black">Lorena Casas</span>Ha comentado una queja</li>
                <hr className="mt-4 border-[1px] border-black"/>
                <li className="text-sm mt-5 text-black"> <span className="font-semibold mr-2 text-black">Juan Rodriguez</span>Ha comentado una queja</li>
            </ul>
        </div>
    )
}

export default Notifications
