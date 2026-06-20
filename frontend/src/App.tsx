import { useEffect, useState } from 'react'

function App() {
  const [products, setProducts] = useState<any[]>([])
  const [categories, setCategories] = useState<any[]>([])
  const [loading, setLoading] = useState(true)

  // Đã cập nhật số PORT dựa theo cấu hình dự án của bạn
  const API_BASE_URL = 'https://localhost:7054/api'

  useEffect(() => {
    // Gọi API lấy dữ liệu song song
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

  return (
    <div className="min-h-screen bg-slate-50">
      {/* Header */}
      <header className="bg-white shadow-sm sticky top-0 z-10">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 flex justify-between items-center">
          <h1 className="text-2xl font-bold text-primary tracking-tight">Drsfan Store</h1>
          <nav className="hidden md:flex space-x-8">
            <a href="#" className="text-slate-600 hover:text-primary font-medium transition-colors">Trang chủ</a>
            <a href="#" className="text-slate-600 hover:text-primary font-medium transition-colors">Sản phẩm</a>
            <a href="#" className="text-slate-600 hover:text-primary font-medium transition-colors">Về chúng tôi</a>
          </nav>
        </div>
      </header>

      {/* Main Content */}
      <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        {loading ? (
          <div className="flex justify-center items-center h-64">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
          </div>
        ) : (
          <div className="flex flex-col md:flex-row gap-8">
            {/* Sidebar Categories */}
            <aside className="w-full md:w-64 shrink-0">
              <div className="bg-white rounded-2xl shadow-sm p-6 border border-slate-100">
                <h2 className="text-lg font-semibold text-slate-800 mb-4 pb-2 border-b border-slate-100">Danh mục</h2>
                <ul className="space-y-3">
                  <li className="text-primary font-medium cursor-pointer">Tất cả sản phẩm</li>
                  {categories.map((cat, idx) => (
                    <li key={idx} className="text-slate-600 hover:text-primary transition-colors cursor-pointer">
                      {cat.name || cat.Name}
                    </li>
                  ))}
                </ul>
              </div>
            </aside>

            {/* Product Grid */}
            <div className="flex-1">
              <div className="mb-6 flex justify-between items-center">
                <h2 className="text-2xl font-bold text-slate-800">Sản phẩm nổi bật</h2>
                <span className="text-sm text-slate-500">{products.length} kết quả</span>
              </div>
              
              <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                {products.map((product, idx) => (
                  <div key={idx} className="bg-white rounded-2xl shadow-sm border border-slate-100 overflow-hidden hover:shadow-md transition-all duration-300 group cursor-pointer">
                    <div className="aspect-[4/3] bg-slate-100 relative overflow-hidden">
                      {product.productImages && product.productImages.length > 0 ? (
                        <img 
                          src={`https://localhost:7054${product.productImages[0].imageUrl.replace(/\\/g, '/')}`} 
                          alt={product.title} 
                          className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500"
                        />
                      ) : (
                        <div className="w-full h-full flex items-center justify-center text-slate-400">
                          No Image
                        </div>
                      )}
                      <div className="absolute top-3 right-3 bg-white/90 backdrop-blur-sm px-2 py-1 rounded-lg text-sm font-bold text-primary shadow-sm">
                        ${product.price}
                      </div>
                    </div>
                    <div className="p-5">
                      <p className="text-xs font-medium text-slate-400 mb-1 uppercase tracking-wider">
                        {product.category?.name || product.category?.Name || "Không phân loại"}
                      </p>
                      <h3 className="text-lg font-semibold text-slate-800 mb-2 line-clamp-1 group-hover:text-primary transition-colors">
                        {product.title || product.Title}
                      </h3>
                      <p className="text-slate-500 text-sm line-clamp-2 mb-4">
                        {product.description?.replace(/<[^>]+>/g, '') || product.Description?.replace(/<[^>]+>/g, '') || "Chưa có mô tả..."}
                      </p>
                      <button className="w-full py-2.5 bg-slate-50 hover:bg-primary hover:text-white text-slate-700 font-medium rounded-xl transition-all duration-300 active:scale-[0.98]">
                        Thêm vào giỏ
                      </button>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          </div>
        )}
      </main>
    </div>
  )
}

export default App
