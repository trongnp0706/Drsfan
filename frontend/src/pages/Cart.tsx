import { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../AuthContext';
import { useNavigate } from 'react-router-dom';
import { Trash2 } from 'lucide-react';

export default function Cart() {
  const { user, token, fetchCartCount } = useContext(AuthContext);
  const [cartItems, setCartItems] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const API_BASE_URL = 'https://localhost:7054/api';

  useEffect(() => {
    if (!user || !token) {
      navigate('/login');
      return;
    }

    fetchCart()
  }, [user, token]);

  const fetchCart = async () => {
    try {
      const res = await fetch(`${API_BASE_URL}/Cart`, {
        headers: { 'Authorization': `Bearer ${token}` }
      });
      const data = await res.json();
      if (data.success) {
        setCartItems(data.data);
      }
      setLoading(false);
    } catch(e) {
      console.error(e);
      setLoading(false);
    }
  }

  const handleRemove = async (cartId: number) => {
    try {
      const res = await fetch(`${API_BASE_URL}/Cart/remove/${cartId}`, {
        method: 'DELETE',
        headers: { 'Authorization': `Bearer ${token}` }
      });
      const data = await res.json();
      if (data.success) {
        if(fetchCartCount) fetchCartCount(token);
        fetchCart();
      }
    } catch(e) {
      console.error(e);
    }
  }

  const calculateTotal = () => {
    return cartItems.reduce((acc, item) => acc + (item.price * item.count), 0);
  }

  if (loading) return <div className="text-center py-12">Loading...</div>;

  return (
    <div className="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
      <h1 className="text-3xl font-bold text-slate-800 mb-8">Shopping Cart</h1>
      
      {cartItems.length === 0 ? (
        <div className="bg-white p-8 rounded-2xl shadow-sm border border-slate-100 text-center">
          <p className="text-slate-500 mb-4">Your cart is empty.</p>
          <button onClick={() => navigate('/')} className="text-[#007bff] hover:underline">Continue Shopping</button>
        </div>
      ) : (
        <div className="bg-white rounded-2xl shadow-sm border border-slate-100 overflow-hidden">
          <div className="hidden sm:grid grid-cols-12 gap-4 p-4 bg-slate-50 border-b border-slate-100 text-sm font-semibold text-slate-600">
            <div className="col-span-6">Product Details</div>
            <div className="col-span-2 text-center">Quantity</div>
            <div className="col-span-2 text-right">Price</div>
            <div className="col-span-2 text-right">Total</div>
          </div>
          
          {cartItems.map((item, idx) => (
            <div key={idx} className="grid grid-cols-1 sm:grid-cols-12 gap-4 p-4 border-b border-slate-100 items-center">
              <div className="col-span-1 sm:col-span-6 flex gap-4">
                <div className="w-20 h-20 bg-slate-100 rounded-lg overflow-hidden shrink-0">
                  {item.product?.productImages?.length > 0 && (
                    <img src={`https://localhost:7054${item.product.productImages[0].imageUrl.replace(/\\/g, '/')}`} alt={item.product.title} className="w-full h-full object-cover" />
                  )}
                </div>
                <div>
                  <h3 className="font-semibold text-slate-800">{item.product?.title || item.product?.Title}</h3>
                  <button onClick={() => handleRemove(item.id)} className="text-red-500 hover:text-red-600 text-sm flex items-center mt-2">
                    <Trash2 size={14} className="mr-1" /> Remove
                  </button>
                </div>
              </div>
              <div className="col-span-1 sm:col-span-2 flex sm:justify-center items-center">
                <span className="sm:hidden font-medium mr-2">Qty: </span>
                <span className="font-semibold">{item.count}</span>
              </div>
              <div className="col-span-1 sm:col-span-2 sm:text-right font-medium text-slate-600">
                <span className="sm:hidden font-medium mr-2">Price: </span>
                ${item.price}
              </div>
              <div className="col-span-1 sm:col-span-2 sm:text-right font-bold text-[#007bff]">
                <span className="sm:hidden font-medium mr-2 text-slate-800">Total: </span>
                ${(item.price * item.count).toFixed(2)}
              </div>
            </div>
          ))}

          <div className="p-6 bg-slate-50 flex flex-col items-end">
            <div className="text-lg text-slate-600 mb-2">Total Amount</div>
            <div className="text-3xl font-bold text-slate-800 mb-4">${calculateTotal().toFixed(2)}</div>
            <button className="bg-[#007bff] hover:bg-blue-600 text-white px-8 py-3 rounded-lg font-semibold transition-colors">
              Proceed to Checkout
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
