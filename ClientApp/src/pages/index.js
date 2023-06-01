import { pageStyle } from '../utils/styles'
import 'bootstrap/dist/css/bootstrap.min.css'; 
import LoginPage from './login';

const Home = () => {
    return (
        <div style={pageStyle}>
            <div style={{ 'textAlign': 'center' }}>
                <h1>Company Management</h1>
                <br />
                <h2>CS415 - Colorado State University</h2>
                <h2>Spring 2023</h2>
                <LoginPage />
            </div>
        </div>
    )
}

export default Home