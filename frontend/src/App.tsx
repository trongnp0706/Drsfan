import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { AuthProvider } from './AuthContext'
import Navbar from './components/Navbar'
import Home from './pages/Home'
import Login from './pages/Login'
import Cart from './pages/Cart'

function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <div className="min-h-screen bg-slate-50 flex flex-col">
          <Navbar />
          <div className="flex-1">
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/login" element={<Login />} />
              <Route path="/cart" element={<Cart />} />
              
              {/* Dummy Admin routes */}
              <Route path="/admin/category" element={<div className="p-8 text-center"><h1 className="text-2xl font-bold">Category Management (Coming Soon)</h1></div>} />
              <Route path="/admin/product" element={<div className="p-8 text-center"><h1 className="text-2xl font-bold">Product Management (Coming Soon)</h1></div>} />
              <Route path="/admin/company" element={<div className="p-8 text-center"><h1 className="text-2xl font-bold">Company Management (Coming Soon)</h1></div>} />
              <Route path="/admin/create-user" element={<div className="p-8 text-center"><h1 className="text-2xl font-bold">Create User (Coming Soon)</h1></div>} />
              <Route path="/admin/manage-user" element={<div className="p-8 text-center"><h1 className="text-2xl font-bold">Manage User (Coming Soon)</h1></div>} />
            </Routes>
          </div>
        </div>
      </AuthProvider>
    </BrowserRouter>
  )
}

export default App
