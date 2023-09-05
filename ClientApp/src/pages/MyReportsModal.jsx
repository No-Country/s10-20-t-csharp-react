import React from 'react';
import ReactDOM from 'react-dom';
import avatarImg from '../assets/Redondo.png';

const MyReportsModal = ({ isOpen, onClose }) => {
    if (!isOpen) return null;

    return ReactDOM.createPortal(
        <div className="fixed inset-0 flex items-center justify-center z-50">
        <div className="fixed inset-0 bg-gray-900 opacity-50"></div>
            <div className="w-[50%] bg-white p-8 rounded-xl z-10">
                
                <button className="mt-4 bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600" onClick={onClose}>Cerrar</button>
            </div>
        </div>,
        document.getElementById('modal-root')
    );
};

export default MyReportsModal;
