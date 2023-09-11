
const Notifications = () => {

    return (
        <div className={`mt-10 absolute -ml-60 shadow-2xl bg-white h-fit rounded-lg w-[22%]`}>
            <ul className="relative p-4 z-10">
                <p className="text-black font-medium text-center w-full">Notificaciones</p>
                <li className="text-sm mt-5 text-black"> <span className="font-semibold mr-2 text-black">Lorena Casas</span>Ha comentado una queja</li>
                <hr className="mt-4 border-[1px] border-black"/>
                <li className="text-sm mt-5 text-black"> <span className="font-semibold mr-2 text-black">Juan Rodriguez</span>Ha comentado una queja</li>
            </ul>
        </div>
    )
}

export default Notifications
