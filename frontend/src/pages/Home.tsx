import { useEffect, useState, useContext } from 'react'
import { AuthContext } from '../AuthContext'
import { useNavigate } from 'react-router-dom'

export default function Home() {
  const [products, setProducts] = useState<any[]>([])
  const [categories, setCategories] = useState<any[]>([])
  const [loading, setLoading] = useState(true)
  const { user, token, fetchCartCount } = useContext(AuthContext)
  const navigate = useNavigate()

  const API_BASE_URL = 'https://localhost:7054/api'

  useEffect(() => {
    Promise.all([
      fetch(`${API_BASE_URL}/Product`).then(res => res.json()),
      fetch(`${API_BASE_URL}/Category`).then(res => res.json())
    ])
      .then(([productRes, categoryRes]) => {
        if (productRes.success) setProducts(productRes.data)
        if (categoryRes.success) setCategories(categoryRes.data)
        setLoading(false)
      })
      .catch(error => {
        console.error("Lỗi khi tải dữ liệu:", error)
        setLoading(false)
      })
  }, [])

  const handleAddToCart = async (productId: number) => {
    if (!user || !token) {
      alert("Vui lòng đăng nhập để thêm vào giỏ hàng!");
      navigate('/login');
      return;
    }

    try {
      const res = await fetch(`${API_BASE_URL}/Cart/add`, {
        method: 'POST',
        headers: { 
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({ productId, count: 1 })
      });
      const data = await res.json();
      if (data.success) {
        // Cập nhật số lượng trên Navbar
        if (fetchCartCount) fetchCartCount(token);
        alert("Đã thêm vào giỏ hàng!");
      } else {
        alert("Lỗi: " + data.message);
      }
    } catch(e) {
      alert("Lỗi kết nối");
    }
  }

  return (
    <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
      {loading ? (
        <div className="flex justify-center items-center h-64">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500"></div>
        </div>
      ) : (
        <div className="flex flex-col md:flex-row gap-8">
          <aside className="w-full md:w-64 shrink-0">
            <div className="bg-white rounded-2xl shadow-sm p-6 border border-slate-100">
              <h2 className="text-lg font-semibold text-slate-800 mb-4 pb-2 border-b border-slate-100">Category</h2>
              <ul className="space-y-3">
                <li className="text-blue-500 font-medium cursor-pointer">All Categories</li>
                {categories.map((cat, idx) => (
                  <li key={idx} className="text-slate-600 hover:text-blue-500 transition-colors cursor-pointer">
                    {cat.name || cat.Name}
                  </li>
                ))}
              </ul>
            </div>
          </aside>

          <div className="flex-1">
            <div className="mb-6 flex justify-between items-center">
              <h2 className="text-2xl font-bold text-slate-800">Welcome to Our Online Store</h2>
            </div>
            <p className="text-slate-500 mb-8">Discover the best appliances and home essentials at unbeatable prices.</p>
            
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
              {products.map((product, idx) => (
                <div key={idx} className="bg-white rounded-2xl shadow-sm border border-slate-100 overflow-hidden hover:shadow-md transition-all duration-300 group">
                  <div className="aspect-[4/3] bg-slate-100 relative overflow-hidden">
                    {product.productImages && product.productImages.length > 0 ? (
                      <img 
                        src={`https://localhost:7054${product.productImages[0].imageUrl.replace(/\\/g, '/')}`} 
                        alt={product.title} 
                        className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500"
                      />
                    ) : (
                      <div className="w-full h-full flex items-center justify-center text-slate-400">No Image</div>
                    )}
                    <div className="absolute top-3 right-3 bg-white/90 backdrop-blur-sm px-2 py-1 rounded-lg text-sm font-bold text-[#007bff] shadow-sm">
                      ${product.price}
                    </div>
                  </div>
                  <div className="p-5 flex flex-col justify-between h-48">
                    <div>
                      <p className="text-xs font-medium text-slate-400 mb-1 uppercase tracking-wider">
                        {product.category?.name || product.category?.Name || "Uncategorized"}
                      </p>
                      <h3 className="text-lg font-semibold text-slate-800 mb-2 line-clamp-1">
                        {product.title || product.Title}
                      </h3>
                      <p className="text-slate-500 text-sm line-clamp-2 mb-4">
                        {product.description?.replace(/<[^>]+>/g, '') || product.Description?.replace(/<[^>]+>/g, '') || "No description"}
                      </p>
                    </div>
                    <button 
                      onClick={() => handleAddToCart(product.id || product.Id)}
                      className="w-full py-2 bg-slate-50 hover:bg-[#007bff] hover:text-white text-slate-700 font-medium rounded-xl transition-all duration-300">
                      Add to Cart
                    </button>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </div>
      )}
    </main>
  )
}
