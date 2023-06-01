import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import './index.css'
import Modal from 'react-modal'
import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-toastify/dist/ReactToastify.css';



const root = ReactDOM.createRoot(document.getElementById('root'))
Modal.setAppElement("#root")
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
)
