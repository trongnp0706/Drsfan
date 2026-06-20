import { useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { ShoppingCart, LogOut, User as UserIcon } from 'lucide-react';
import { AuthContext } from '../AuthContext';

export default function Navbar() {
  const { user, cartCount, logout } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <nav className="bg-[#007bff] text-white shadow-md sticky top-0 z-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          <div className="flex items-center space-x-8">
            <Link to="/" className="flex items-center gap-2 font-bold text-xl">
              <div className="w-8 h-8 bg-white text-[#007bff] rounded-full flex items-center justify-center font-black">
                D
              </div>
              Drsfan
            </Link>
            
            <div className="hidden md:flex space-x-6 items-center">
              <Link to="/" className="hover:text-blue-200 transition-colors">Home</Link>
              {user && <Link to="/orders" className="hover:text-blue-200 transition-colors">Manage Order</Link>}
              
              {user?.roles?.includes('Admin') && (
                <div className="relative group cursor-pointer">
                  <span className="hover:text-blue-200 flex items-center gap-1">
                    Management <span className="text-xs">▼</span>
                  </span>
                  <div className="absolute left-0 mt-2 w-48 bg-white rounded-md shadow-lg py-1 z-50 hidden group-hover:block text-slate-800">
                    <Link to="/admin/category" className="block px-4 py-2 text-sm hover:bg-slate-100">Category</Link>
                    <Link to="/admin/product" className="block px-4 py-2 text-sm hover:bg-slate-100">Product</Link>
                    <Link to="/admin/company" className="block px-4 py-2 text-sm hover:bg-slate-100">Company</Link>
                    <div className="border-t border-slate-100 my-1"></div>
                    <Link to="/admin/create-user" className="block px-4 py-2 text-sm hover:bg-slate-100">Create new user</Link>
                    <Link to="/admin/manage-user" className="block px-4 py-2 text-sm hover:bg-slate-100">Manage user</Link>
                  </div>
                </div>
              )}
            </div>
          </div>

          <div className="flex items-center space-x-6">
            <Link to="/cart" className="flex items-center hover:text-blue-200 transition-colors">
              <ShoppingCart size={20} className="mr-1" />
              <span>({cartCount})</span>
            </Link>

            {user ? (
              <div className="flex items-center space-x-4">
                <span className="hidden md:inline text-sm">Hello {user.email}!</span>
                <button 
                  onClick={handleLogout}
                  className="bg-red-500 hover:bg-red-600 text-white px-3 py-1.5 rounded-md text-sm font-medium transition-colors flex items-center"
                >
                  <LogOut size={16} className="mr-1"/> Logout
                </button>
              </div>
            ) : (
              <Link to="/login" className="bg-white text-[#007bff] hover:bg-blue-50 px-4 py-1.5 rounded-md text-sm font-bold transition-colors flex items-center">
                <UserIcon size={16} className="mr-1"/> Login
              </Link>
            )}
          </div>
        </div>
      </div>
    </nav>
  );
}
